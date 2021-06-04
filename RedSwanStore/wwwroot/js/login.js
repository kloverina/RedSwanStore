
let form = document.querySelector('.form');
let inputs = form.querySelectorAll('input:not([type=hidden])')
let email = form.querySelector('.form-element__email'); //delete me later
let password = form.querySelector('.form_element__password'); //and me too
let pass_match = form.querySelector('input.form_element__checkpass');
let submit_button = form.querySelector('.form_element__button');
let pass_match_warning = form.querySelector('p.form_element__checkpass');


function isValid(inputs) {
    let valid = true;
    
    
    inputs.forEach(function (input, i, inputs) {
        if (input.value === '' || !input.validity.valid ) {
            valid = false;
        }
    })

    if (!passMatch()) valid = false;
    
    return valid;
}

function passMatch(){
    if (!pass_match) 
        return true;
    if(password.value !== pass_match.value && pass_match.value !== ''){
        pass_match_warning.classList.add('active');
        return false
    }
    else
        pass_match_warning.classList.remove('active');
    
    return true;
}

function checkboxClick(labels) {
    for(let i = 0; i< labels.length; i++){
        let label = labels[i];
        
            
        
        label.onclick= function (){
            let checkbox = label.querySelector("input");
            let custom_checkbox = label.querySelector('div');
            if (checkbox.checked)
                custom_checkbox.classList.add('checked');
            else
                custom_checkbox.classList.remove('checked');

            if (!checkbox.hasAttribute('value')) {
                checkbox.setAttribute('value', 'true');
            }
            else {
                checkbox.setAttribute(
                    'value',
                    checkbox.getAttribute('value') === 'true' ? 'false' : 'true'
                );
            }
        }
    }
}

form.addEventListener("input", function (event) {
    
   if (isValid(inputs))
       submit_button.classList.add('active');
   else
       submit_button.classList.remove('active');
})

//clicks on custom checkboxes
let labels = form.querySelectorAll('label.custom-checkbox');
if (labels)
    checkboxClick(labels);


// the callback that is called when the server sends response to ajax
onSignUp = function (xhr) {
    if(xhr.responseText === "email") {
        //TODO: add display of a message that an account has already been registered to this email address
        console.log("ТАКОЙ ЕМЕЙЛ УЖЕ ЗАНЯТ, ДУРАК БЛИН!");

        // leave it or delete it (at your discretion)
        document.querySelector('#check_password').value = '';
        document.querySelector('#password').value ='';
    }
    else if (xhr.responseText.startsWith("{")) {
        //TODO: ? add display of something if wrong data was send to the server and its validation failed ?

        // received data is json so I parsed it and made detailed description here especially for you!
        // validation instance looks like if you do the next thing:
        // let validation = {
        //      success: <bool>,
        //      result: {
        //          Name: {
        //              HasInvalidCharacters: <bool>,
        //              HasInvalidLength: <bool>
        //          },
        //          Surname: {
        //              HasInvalidCharacters: <bool>,
        //              HasInvalidLength: <bool>
        //          },
        //          Login: { },
        //          Email: { },
        //          Password: { }
        //      }
        // }
        // I HAVE NOT TESTED the validation instance, but it seems that it's parsed with no problems
        // So I suppose you just can use it like 'if (!validation.result.Name.HasInvalidCharacters)' or something idk...
        let validation = JSON.parse(xhr.responseText);
        console.log(validation.result.Name.HasInvalidCharacters);

        // leave it or delete it (at your discretion)
        document.querySelector('#check_password').value = '';
        document.querySelector('#password').value ='';
    }
    else
        window.location.replace(xhr.responseText);
}


onLogin = function (xhr) {
    //TODO: Add display of 'incorrect email' or 'incorrect password' messages
    
    // received data is json so I parsed it and made detailed description here especially for you!
    // login result instance looks like if you do the next thing:
    // let loginResult = {
    //      isCorrectEmail: <bool>,
    //      isCorrectPassword: <bool>,
    //      redirectLink: <string>
    // }
    let loginResult = JSON.parse(xhr.responseText);
    
    if (!loginResult.isCorrectEmail) {
        console.log("Аккаунта с указанной электронной почтой не существует!");
    }
    else if (!loginResult.isCorrectPassword) {
        console.log("Неверный пароль");
    }
    else
        window.location.replace(loginResult.redirectLink);
}


onRecovery = function (xhr) {
    //TODO: Add display of 'incorrect email' message

    // received data is json so I parsed it and made detailed description here especially for you!
    // recovery result instance looks like if you do the next thing:
    // let recoveryResult = {
    //      isCorrectEmail: <bool>,
    //      errorMessage: <string>
    //      redirectLink: <string>
    // }
    let recoveryResult = JSON.parse(xhr.responseText);

    if (!recoveryResult.isCorrectEmail) {
        console.log("Аккаунта с указанной электронной почтой не существует!");
    }
    else if (recoveryResult.errorMessage !== "") { // this is debug-only (not needed to be seen by user)
        console.log(recoveryResult.errorMessage);
    }
    else
        window.location.replace(recoveryResult.redirectLink);
}
