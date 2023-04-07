
    function validateForm() {
    var name = document.getElementById("Name").value;
    var email = document.getElementById("Email").value;
    var phone = document.getElementById("Phone").value;
    var question = document.getElementById("Security_Question").value;
    var password = document.getElementById("Password").value;
    var role = document.getElementById("Role").value;
    var gender = document.getElementById("Gender").value;
    var image = document.getElementById("poster").value;

    if (name == "") {
        alert("Name must be filled out");
    return false;
    }

    if (email == "") {
        alert("Email must be filled out");
    return false;
    }

    if (phone == "") {
        alert("Phone number must be filled out");
    return false;
    }

    if (question == "") {
        alert("Secret key must be filled out");
    return false;
    }

    if (password == "") {
        alert("Password must be filled out");
    return false;
    }

    if (role == "") {
        alert("Role must be selected");
    return false;
    }

    if (gender == "") {
        alert("Gender must be selected");
    return false;
    }

    if (image == "") {
        alert("Please select a profile picture");
    return false;
    }
}

