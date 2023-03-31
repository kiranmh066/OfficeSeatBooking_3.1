function validateForm() {
    // Get values from input fields
    var firstName = document.getElementById("FirstName").value;
    var lastName = document.getElementById("LastName").value;
    var email = document.getElementById("Email").value;
    var empID = document.getElementById("Emp").value;
    var address = document.getElementById("Place").value;
    var gender = document.querySelector('input[name="inlineRadioOptions"]:checked');
    var bloodGroup = document.querySelector('.select[name="BloodGroup"]');
    var role = document.querySelector('.select[name="Role"]');
    var designation = document.getElementById("Designation").value;
    var secretKey = document.getElementById("SecretKey").value;

    // Check that required fields are filled out
    if (firstName == "") {
        alert("Please enter your first name.");
        return false;
    }
    if (lastName == "") {
        alert("Please enter your last name.");
        return false;
    }
    if (email == "") {
        alert("Please enter your email address.");
        return false;
    }
    if (empID == "") {
        alert("Please enter your employee ID.");
        return false;
    }
    if (address == "") {
        alert("Please enter your address and place.");
        return false;
    }
    if (!gender) {
        alert("Please select your gender.");
        return false;
    }
    if (bloodGroup.selectedIndex == 0) {
        alert("Please select your blood group.");
        return false;
    }
    if (role.selectedIndex == 0) {
        alert("Please select your role.");
        return false;
    }
    if (designation == "") {
        alert("Please enter your designation.");
        return false;
    }
    if (secretKey == "") {
        alert("Please enter your secret key.");
        return false;
    }

    // If all fields are filled out, return true
    return true;
}
