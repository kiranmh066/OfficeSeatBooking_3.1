function validateForm() {
    const empId = document.getElementById("text").value;
  
    // Check if empId field is empty
    if (empId === "") {
      alert("Please enter EMP_ID.");
      return false;
    }
  
    // Check if empId field contains only numbers
    if (!/^\d{4}$/.test(empId)) {
      alert("Please enter a valid EMP_ID (only 4 digits number).");
      return false;
    }
  
    return true;
  }
  
  const form = document.querySelector("form");
  form.addEventListener("submit", function(event) {
    event.preventDefault(); // prevent form submission if validation fails
    if (validateForm()) {
      // submit form if validation passes
      alert("Form submitted successfully.");
      // add code to submit form here
    }
  });
  