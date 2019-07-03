$(document).ready(function () {
    //JavaScript for the login form
    $("#login-form").on("submit", function (e) {
        if ($("#Email").val() === "") {
            e.preventDefault();
            $("#Email").addClass("is-invalid");
        }
        if ($("#Password").val() === "") {
            e.preventDefault();
            $("#Password").addClass("is-invalid");
        }
    });

    //Add warning styling to Email input if it is empty when not in focus
    $("#Email").on("focus", function () {
        $("#Email").removeClass("is-invalid");
    });
    $("#Email").on("focusout", function () {
        if ($("#Email").val() === "") {
            $("#Email").addClass("is-invalid");
        }
    });

    //Add warning styling to Password input if it is empty when not in focus
    $("#Password").on("focus", function () {
        $("#Password").removeClass("is-invalid");
    });
    $("#Password").on("focusout", function () {
        if ($("#Password").val() === "") {
            $("#Password").addClass("is-invalid");
        }
    });

    //Javascript for the account creation form
    $("#signup-form").on("submit", function (e) {
        if ($("#NewUsername").val() === "") {
            e.preventDefault();
            $("#NewUsername").addClass("is-invalid");
        }
        if ($("#NewEmail").val() === "") {
            e.preventDefault();
            $("#NewEmail").addClass("is-invalid");
        }
        if ($("#NewPassword").val() === "") {
            e.preventDefault();
            $("#NewPassword").addClass("is-invalid");
        }
    });

    //Add warning styling to NewUsername if it is empty when not in focus
    $("#NewUsername").on("focus", function () {
        $("#NewUsername").removeClass("is-invalid");
    });
    $("#NewUsername").on("focusout", function () {
        if ($("#NewUsername").val() === "") {
            $("#NewUsername").addClass("is-invalid");
        }
    });

    //Add warning styling to NewEmail if it is empty when not in focus
    $("#NewEmail").on("focus", function () {
        $("#NewEmail").removeClass("is-invalid");
    });
    $("#NewEmail").on("focusout", function () {
        if ($("#NewEmail").val() === "") {
            $("#NewEmail").addClass("is-invalid");
        }
    });

    //Add warning styling to NewPassword if it is empty when not in focus
    $("#NewPassword").on("focus", function () {
        $("#NewPassword").removeClass("is-invalid");
    });
    $("#NewPassword").on("focusout", function () {
        if ($("#NewPassword").val() === "") {
            $("#NewPassword").addClass("is-invalid");
        }
    });
});