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
	// //Reset Inputfield code.
	// function clearFunc()
	// {
	// 	document.getElementById("email1").value="";
	// 	document.getElementById("password1").value="";
	// }


