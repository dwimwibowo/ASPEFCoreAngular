$(document).ready(function() {
    console.log("Hello Pluralsight");

    //var theForm = document.getElementById("the-form");
    //var theForm = $("#theForm");
    //theForm.hidden = true;
    //theForm.hide();

    //var button = document.getElementById("buy-button");
    var button = $("#buyButton");

    //button.addEventListener("click", () => {
    //    alert("Buying Item");
    //})
    button.on("click", function () {
        console.log("Buying item");
        alert("Buying Item");
    });

    //var productInfo = document.getElementsByClassName("product-props");
    var productInfo = $(".product-props li");

    //var listItems = productInfo[0].children;
    productInfo.on("click", function () {
        console.log("You click on " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");
    $loginToggle.on("click", function () {
        //$popupForm.toggle(['slow']);
        $popupForm.toggle(1000);
    });

    /*** add active class and stay opened when selected ***/
    var url = window.location;

    // for sidebar menu entirely but not cover treeview
    $('ul.nav-sidebar a').filter(function () {
        if (this.href) {
            return this.href == url || url.href.indexOf(this.href) == 0;
        }
    }).addClass('active');

    // for the treeview
    $('ul.nav-treeview a').filter(function () {
        if (this.href) {
            return this.href == url || url.href.indexOf(this.href) == 0;
        }
    }).parentsUntil(".nav-sidebar > .nav-treeview").addClass('menu-open').prev('a').addClass('active');
});