$(".remove").click(function () {
  var el = $(this);
  el.parent().parent().addClass("removed");
  window.setTimeout(
    function () {
      el.parent().parent().slideUp('fast', function () {
        el.parent().parent().remove();
        if ($(".product").length == 0) {
          if (check) {
            $("#cart").html("<h1>The shop does not function, yet!</h1><p>If you liked my shopping cart, please take a second and heart this Pen on <a href='https://codepen.io/ziga-miklic/pen/xhpob'>CodePen</a>. Thank you!</p>");
          } else {
            $("#cart").html("<h1 class='empty'>No products!</h1>");
          }
        }
        changeTotal();
      });
    }, 200);
});


let userIcon = document.querySelector("#app-menu .icons ul li .user")
let loginRegister = document.querySelector("#app-menu .login-register")

userIcon.addEventListener("click", function (e) {
  e.preventDefault()
  loginRegister.classList.toggle("d-none")
})





let searchIcon = document.querySelector("#app-menu .icons ul li .glass")
let search = document.querySelector("#app-menu .search")
searchIcon.addEventListener("click", function (e) {
  e.preventDefault()
  search.classList.toggle("d-none")
})



