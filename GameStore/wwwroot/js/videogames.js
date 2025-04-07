let table;

document.addEventListener("DOMContentLoaded", async function(){
    await loadDataTable();
})

async function loadDataTable(){
    let response = await fetch("/Admin/admin/getall");
    let videogames = await response.json();
    console.log(videogames);
    
     table = new DataTable('#gamesTable', {
        data: videogames.data,
        columns: [
            { data: 'name', width: "15%" },
            { data: 'stock', width: "15%" },
            { data: 'releaseDate' , width: "15%"},
            { data: 'genre.name', width: "15%" },
            { data: 'price', width: "15%" },
            {
                data: 'videoGameId',width: "20%",
                render: function (data) {
                    return `<div class="btn-group">
                                <a class="btn btn-sm btn-outline-primary me-1" href="/Admin/admin/upsert?id=${data}"> 
                                        <i class="bi bi-pencil"></i>
                                    </a>
                                    <button class="btn btn-sm btn-outline-danger" onclick="Delete('/Admin/admin/delete?id=${data}')">
                                        <i class="bi bi-trash"></i>
                                    </button>
                            </div>`
                }
            }
        ]
    });
}


async function Delete(url){
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url,{
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json"
                }
            })
                .then(res => res.json())
                .then(data => {
                    console.log(data);
                    if (data) {
                        swalWithBootstrapButtons.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        setTimeout(function () {
                            window.location.reload();
                        },2000)
                    }
                })
            
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your Game is safe :)",
                icon: "error"
            });
        }
    });
}

