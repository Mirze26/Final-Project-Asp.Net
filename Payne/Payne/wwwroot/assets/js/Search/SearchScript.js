

"use strict";

document.querySelector("#app-menu .search .fa-magnifying-glass").addEventListener("click", function () {


    console.log("dsad")
    if (document.querySelector("header #app-menu .search .test").value.trim() != "") {  
        console.log("dssdas")
        let searchText = document.querySelector("header #app-menu .search span").value;
        let url = `/Search/SearchByCars?searchText=${searchText}`;

        window.location.assign(url);

    }
});

//enter
document.querySelector("header #navbar .search .search-input input").addEventListener("keydown", function (event) {
    if (event.keyCode === 13) {
        if (document.querySelector("header #navbar .search .search-input input").value.trim() != "") {
            let searchText = document.querySelector("header #navbar .search .search-input input").value;
            let url = `/Search/SearchByCars?searchText=${searchText}`;

            window.location.assign(url);

        }
    }
});