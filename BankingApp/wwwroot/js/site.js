//Intended for use inside form onsubmit events
//Checks to make sure that ALL form inputs are populated
//If ANY are empty adds is-invalid class to them and prevents form submission
function checkAllFormInputs(form, event) {
    form.filter(":input").each(function () {
        if (this.value === "") {
            event.preventDefault();
            this.classList.add("is-invalid");
        }
    });
}

//Add warning styling to inputs if they are empty when not focused and removes them when the input is focused
function addInputRequiredStyling(input) {
    input.on("focus", function (e) {
        e.target.classList.remove("is-invalid");
    });
    input.on("focusout", function (e) {
        if (e.target.value === "") {
            e.target.classList.add("is-invalid");
        }
    });
}
