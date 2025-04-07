let table;
let newOrders;
document.addEventListener("DOMContentLoaded", async function(){
    await loadDataTable();
})

async function loadDataTable(){
    let response = await fetch("/Admin/admin/getallorders");
    let orders = await response.json();
    newOrders = orders.data.filter(o => o.accepted === false);
    console.log(orders);
    console.log(newOrders);

    table = new DataTable('#ordersTable', {
        data: newOrders,
        columns: [
            { data: 'orderDate', width: "15%" },
            { data: 'user.userName', width: "15%" },
            {
                data: 'orderId',width: "20%",
                render: function (data) {
                    return `<div class="btn-group">
                                <a class="btn btn-sm btn-outline-primary me-1" href="/Admin/Admin/orderinfo?id=${data}"> 
                                        <i class="bi bi-eye"></i>
                                    </a>
                                    <button class="btn btn-sm btn-outline-success btn-accept-order" onclick="AcceptOrder('/Admin/admin/acceptorder?id=${data}',${data})">
                                        <i class="bi bi-check-lg"></i>
                                    </button>
                            </div>`
                }
            }
        ]
    });
}


async function AcceptOrder(url,orderId){
    let orderOfUser = newOrders.filter(o => o.orderId == orderId);
    let userId = orderOfUser[0].userId;
    console.log(orderId);
    let parsedOrderID = parseInt(orderId);
    console.log(parsedOrderID);
    signalRConnection.invoke("SendNotification",userId,parsedOrderID)
        .then(()=>console.log("Se ha aceptado su pedido"))
        .catch(error=>{
        console.log(error);
    });
    await fetch(url,{
        method: "POST"
    }).then((res) => res.status)
        .then(status => {
            if(status === 200){
                Swal.fire({
                    title: "Success!",
                    text: "You accepted the order!",
                    icon: "success"
                });
                setTimeout(()=>{
                    location.reload();
                }, 2000)
            }
        })
}

