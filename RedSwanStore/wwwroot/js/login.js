
let form = document.querySelector('.form');
let inputs = form.querySelectorAll('input')
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
            let checkbox = label.querySelector('div');
            checkbox.classList.toggle('checked');

            let realCheckbox = label.querySelector('input');
            
            if (!realCheckbox.hasAttribute('value')) {
                realCheckbox.setAttribute('value', 'true');
            }
            else {
                realCheckbox.setAttribute(
                    'value',
                    realCheckbox.getAttribute('value') === 'true' ? 'false' : 'true'
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


// the callback that is called when the server sends response to ajax
onReceiveAnswer = function (xhr) {
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


//clicks on custom checkboxes
let labels = form.querySelectorAll('label.custom-checkbox');
if (labels)
    checkboxClick(labels);



