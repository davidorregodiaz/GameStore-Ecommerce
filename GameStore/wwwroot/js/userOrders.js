let table;

document.addEventListener("DOMContentLoaded", async function(){
    await loadDataTable();
})

async function loadDataTable(){
    let response = await fetch("/Customer/Home/OrdersRecordJson");
    let orders = await response.json();
    console.log(orders);

    table = new DataTable('#ordersTable', {
        data: orders.data,
        columns: [
            { data: 'orderDate', width: "15%" },
            { data: 'accepted', width: "15%" ,
                render : function (data) {
                    if(data){
                        return `<div class="d-flex gap-2"><i class="bi bi-check-circle-fill"></i><p>Accepted</p></div>`;
                    }else{
                        return `<div class="d-flex gap-2"><i class="bi bi-x-lg"></i><p>Pending</p></div>`;
                    }
                    
                }
            },
            {
                data: 'orderId',width: "20%",
                render: function (data) {
                    return `<div class="btn-group">
                                <a class="btn btn-sm btn-outline-primary me-1" href="/Customer/Home/orderinfo?id=${data}"> 
                                        <i class="bi bi-eye"></i>
                                    </a>
                            </div>`
                }
            }
        ]
    });
}