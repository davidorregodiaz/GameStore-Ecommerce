// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
document.addEventListener("DOMContentLoaded",function() {
    checkCart();
})

// Write your JavaScript code.

var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

signalRConnection.start()
    .then(()=>console.log("Conectado a signal R"))
    .catch((err)=>{console.log(err)});



    signalRConnection.on("ReceiveNotification", function (notification){
        console.log(notification);
        
        const listItem = document.createElement("div");
        console.log(listItem);
        console.log(document.querySelector(".notification-container"));
        listItem.innerHTML = `<div class="list-group-item list-group-item-action">
                                                    <a href="/Customer/Home/orderinfo?id=${notification.orderId}" class="my-list-item">
                                                        <div class="d-flex w-100 justify-content-between">
                                                            <h5 class="mb-1">${notification.message}</h5>
                                                            <small class="text-body-secondary">Justo Ahora</small>
                                                        </div>
                                                        <p class="mb-1">Su solicitud ha sido aceptada exitosamente</p>
                                                    </a>
                                                    <small class="text-body-secondary mark-text" role="button">Mark as read.</small>
                                                </div>`;
        
        console.log(listItem);
        let notiCounter = parseInt(document.querySelector(".noti-counter").getAttribute("value"));
        console.log(notiCounter);
        let notiHTMLCount = document.querySelector(".my-badge");
        notiCounter = notiCounter+1;
        document.querySelector(".noti-counter").setAttribute("value",notiCounter.toString());
        notiHTMLCount.innerHTML = notiCounter.toString();
        document.querySelector(".notification-container").prepend(listItem);
    })




setTimeout(() => {
    const toastHTML = document.querySelector("#liveToast");
    if(toastHTML.classList.contains("show")) {
        toastHTML.classList.remove("show");
        toastHTML.classList.remove("fade-in");
        toastHTML.classList.add("hidden");
    }}, 5000);


let cartCount;
async function addToCart(url){
    console.log(url);
    await fetch(url,{
        method: "POST"
    })
    .then(res => res.json())
    .then(data => {
        console.log(data);
        if(data.success){
            cartCount+=1;
            sessionStorage.setItem("cartCount", JSON.stringify(cartCount));
            checkCart();
            Swal.fire({
                title: "Good job!",
                text: "You added to your cart.!",
                icon: "success"
            });
        }else{
            Swal.fire({
                title: "An error occured!",
                text: "You already have this item!",
                icon: "error"
            });
        }
    });
    
}

function checkCart() {
    const cartNoti=document.querySelector(".cart-notification");
    cartCount = parseInt(sessionStorage.getItem("cartCount")) || 0;
    
    if(cartCount !== 0){
        cartNoti.style.display= "block";
    }else{
        cartNoti.style.display = "none";
    }
}

function setCartNoti(){
    cartCount = 0;
    sessionStorage.setItem("cartCount", JSON.stringify(cartCount));
    checkCart();
}

