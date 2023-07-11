
let swiper = new Swiper(".mySwiper", {
  loop: true,
  slidesToScroll: 1,
  spaceBetween: 30,
  centeredSlides: true,
  autoplay: {
    delay: 2500,
    disableOnInteraction: false,
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },

});


let userIcon = document.querySelector("#app-menu .icons ul li .user")
let loginRegister = document.querySelector("#app-menu .login-register")

userIcon.addEventListener("click",function(e){
  e.preventDefault()
      loginRegister.classList.toggle("d-none")
})




let searchIcon = document.querySelector("#app-menu .icons ul li .glass")
let search = document.querySelector("#app-menu .search")
searchIcon.addEventListener("click", function(e){
  e.preventDefault()
  search.classList.toggle("d-none")
})


document.querySelector("#app-menu .search .fa-magnifying-glass").addEventListener("click", function () {


    if (document.querySelector("header #app-menu .search input").value.trim() != "") {
        let searchText = document.querySelector("header #app-menu .search input").value;
        let url = `/Search/SearchByProducts?searchText=${searchText}`;

        window.location.assign(url);
    }
});



