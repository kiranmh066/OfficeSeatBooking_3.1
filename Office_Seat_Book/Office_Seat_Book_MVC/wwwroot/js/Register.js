function validateForm() {
    // Get input values
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var phone = document.getElementById("phone").value;
    var secretkey = document.getElementById("secretkey").value;
    var designation = document.getElementById("designation").value;
    var role = document.getElementById("role").value;
    var password = document.getElementById("password").value;
    var confirmpassword = document.getElementById("confirmpassword").value;
    var gender = document.querySelector(".gender").value;

    // Check if input values are empty
    if (name == "" || email == "" || phone == "" || secretkey == "" || designation == "" || role == "" || password == "" || confirmpassword == "" || gender == "Gender") {
        alert("Please fill in all fields");
        return false;
    }

    // Check if name contains only letters and spaces
    if (!/^[a-zA-Z\s]+$/.test(name)) {
        alert("Name must contain only letters and spaces");
        return false;
    }

    // Check if email is valid
    if (!/\S+@\S+\.\S+/.test(email)) {
        alert("Please enter a valid email address");
        return false;
    }

    // Check if phone number is valid
    if (!/^\d{10}$/.test(phone)) {
        alert("Please enter a valid phone number");
        return false;
    }

    // Check if secret key is valid
    if (secretkey != "123456") {
        alert("Please enter a valid secret key");
        return false;
    }

    // Check if passwords match
    if (password != confirmpassword) {
        alert("Passwords do not match");
        return false;
    }

    // If all validations pass, submit form
    alert("Form submitted successfully");
    return true;
}