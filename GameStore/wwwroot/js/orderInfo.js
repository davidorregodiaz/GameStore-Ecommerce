
document.addEventListener("DOMContentLoaded", async function() {
    await loadOrderInfo();
});

async function loadOrderInfo() {
    let id = parseInt(document.getElementById("orderId").getAttribute("value"));
    console.log(id);
    let innerBody ="";
    
    await fetch(`/Customer/Home/orderinfojson?id=${id}`)
    .then(res => res.json())
    .then(data => {
        console.log(data);
        console.log(data.data.videoGameOrders);
        for(let videogame of data.data.videoGameOrders){
            console.log(videogame.orderId);
            console.log(videogame.videoGame);
            innerBody += `<tr>
                                <td>
                                    <div class="d-flex align-items-center gap-5">
                                        <div class="order-item-img-container d-flex align-items-center" style="width: 50px;height: 50px">
                                            <img src=${videogame.videoGame.imageUrl} style="max-width: 50px;max-height: 50px" class="product-img me-3 object-fit-cover" alt="Product">
                                        </div>
                                        <div>
                                            <h6 class="mb-0">${videogame.videoGame.name}</h6>
                                            <small class="text-muted"></small>
                                        </div>
                                    </div>
                                </td>
                                <td>${videogame.videoGame.price}</td>
                                <td class="text-end">${videogame.videoGame.price}</td>
                            </tr>`
        }
        let innerTable = document.querySelector(".order-info-table-body");
        innerTable.innerHTML = innerBody;
        console.log(innerTable);
    })
}
