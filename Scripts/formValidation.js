///<reference path="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.jsLinks to an external site." />
//XML reference gives us intellisense for jQuery

function validateForm(event) {
    //with custom js validation we will require each field
    var name = document.forms['main-contact-form']['name'].value;
    var email = document.forms['main-contact-form']['email'].value;
    var subject = document.forms['main-contact-form']['subject'].value;
    var message = document.forms['main-contact-form']['message'].value;

    //get error message <div>
    var nameVal = document.getElementById('nameVal');
    var emailVal = document.getElementById('emailVal');
    var subjectVal = document.getElementById('subjectVal');
    var messageVal = document.getElementById('messageVal');

    //Declare regular expression for email
    var emailRegEx = new RegExp(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/);
    //Test all of our conditions including checking for a valid email

    //MINI LAB
    //use regular expression to verify that first names only include alphabetic characters
    var nameRegEx = new RegExp(/^[a-zA-Z]+((\s|\-)[a-zA-Z]+)?$/);


    if (name.length == 0 || email.length == 0 || subject.length == 0 || message.length == 0 ||
        emailRegEx.test(email) || nameRegEx.test(name)) {

        //error messages for each required field
        if (name.length == 0) {
            nameVal.textContent = "* Name is required";
        }
        else {
            nameVal.textContent = "";
        }
        
        if ((!nameRegEx.test(name)) && name.length > 0 ) {
            nameVal.textContent = "*Please use only letters"
        }
        if ((nameRegEx.test(name)) && name.length > 0) {
            nameVal.textContent = "";
        }
        if (email.length == 0) {
            emailVal.textContent = "* Email is Required";
        }
        else {
            emailVal.textContent = "logic error";
        }
        if (!emailRegEx.test(email) && email.length > 0) {
            emailVal.textContent = "*Must be a vaild email address";
        }
        if (emailRegEx.test(email) && email.length > 0) {
            emailVal.textContent = "";
        }

        if (subject.length == 0) {
            subjectVal.textContent = "* Subject is required";
        }
        else {
            subjectVal.textContent = "";
        }

        if (message.length == 0) {
            messageVal.textContent = "* Message is Required ";
        }
        else {
            messageVal.textContent = "";
        }

        event.preventDefault();//prevents the form from submitting if statement is true.
    }//end if allcondition

}//end function validateForm