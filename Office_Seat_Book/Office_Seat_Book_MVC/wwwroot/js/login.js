function login() {
	var email = document.getElementById("email1").value;
	var password = document.getElementById("password1").value;
	var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	if (email == '') {
		alert("please enter user name.");
	}
	else if (password == '') {
		alert("enter the password");
	}
	else if (!filter.test(email)) {
		alert("Enter valid email id.");
	}
	else if (password.length < 6 || password.length > 6) {
		alert("Password min and max length is 6.");
	}
	else {
		alert('Login Successfull!!');
		//Redirecting to other page or webste code or you can set your own html page.
		window.location = " ";
	}
}

// Get the checkbox element
const rememberMeCheckbox = document.getElementById('form');

// Check if the user has previously checked the checkbox
if (localStorage.getItem('rememberMe') === 'true') {
	rememberMeCheckbox.checked = true;
}

// Add an event listener to the checkbox
rememberMeCheckbox.addEventListener('click', function () {
	// Save the checkbox state in the local storage
	localStorage.setItem('rememberMe', this.checked);
});

//Reset Inputfield code.
function clearFunc() {
	document.getElementById("email1").value = "";
	document.getElementById("password1").value = "";
}

function showSuccessModal() {
	$('#myModal').modal('show');
}



