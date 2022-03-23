// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function displayRow(appointmentID) {
    document.getElementById("hidden" + appointmentID).hidden = false;
}

function closeRow(appointmentID) {
    document.getElementById("hidden" + appointmentID).hidden = true;
}