// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#btn1").click(function () {
    $("a").removeClass("active");
    $("#btn1").addClass("active");
    $("#index1").removeClass("invisible");
    $("#index2").addClass("invisible");
})

$("#btn2").click(function () {
    $("a").removeClass("active");
    $("#btn2").addClass("active");

    $("#index2").removeClass("invisible");
    $("#index1").addClass("invisible");
})

$("#btn3").click(function () {
    $("a").removeClass("active");
    $("#btn3").addClass("active");
})

$("#btn4").click(function () {
    $("a").removeClass("active");
    $("#btn4").addClass("active");
})