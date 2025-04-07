let table;

document.addEventListener("DOMContentLoaded", async function(){
    await loadDataTable();
})

async function loadDataTable(){
    let response = await fetch("/Admin/admin/getallorders");
    let orders = await response.json();
    let newOrders = orders.data.filter(o => o.accepted === true);
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
                                   
                            </div>`
                }
            }
        ]
    });
}