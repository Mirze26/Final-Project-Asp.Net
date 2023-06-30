

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