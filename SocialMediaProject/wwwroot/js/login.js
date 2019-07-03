//JavaScript here
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