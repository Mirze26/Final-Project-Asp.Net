const swiper = new Swiper('.swiper', {
    slidesPerView: 1,
  
  
  
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    virtual: {
      slides: (function () {
        const slides = [];
        for (var i = 0; i < 2; i += 1) {
          slides.push('Slide ' + (i + 1));
        }
        return slides;
      })(),
    },
  });




const allHoverImages = document.querySelectorAll('.hover-container div img');
const imgContainer = document.querySelector('.img-container');
console.log(allHoverImages);
window.addEventListener('DOMContentLoaded', () => {
    allHoverImages[0].parentElement.classList.add('active');
});

allHoverImages.forEach((image) => {
    image.addEventListener('mouseover', () => {
        imgContainer.querySelector('img').src = image.src;
        resetActiveImg();
        image.parentElement.classList.add('active');
    });
});

function resetActiveImg() {
    allHoverImages.forEach((img) => {
        img.parentElement.classList.remove('active');
    });
}



let userIcon = document.querySelector(".menu-area #app-menu .icons ul li .user")
let loginRegister = document.querySelector(".menu-area #app-menu .login-register")

userIcon.addEventListener("click", function (e) {
    e.preventDefault()
    loginRegister.classList.toggle("d-none")
})




let searchIcon = document.querySelector("#app-menu .icons ul li .glass")
let search = document.querySelector("#app-menu .search")
searchIcon.addEventListener("click", function(e){
  e.preventDefault()
  search.classList.toggle("d-none")
})













