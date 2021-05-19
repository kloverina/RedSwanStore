

//фильтр
let options = document.querySelectorAll('.options__item');

for (let i = 0; i < options.length; i++) {
    let option = options[i];
    let optionName = option.querySelector('span');
    optionName.onclick = function () {

        option.classList.toggle('options__item_hidden');

    }

}



//дропдаун сортировки
let dropdown = document.querySelector('.dropdown');
let sorting_button = dropdown.querySelector('.dropdown_button');
let submenu = dropdown.querySelector('.dropdown__submenu');
let submenu_buttons = submenu.querySelectorAll('.submenu__button');
let title = sorting_button.querySelector('.dropdown_button__toggleLabel');

sorting_button.onclick = function () {

    submenu.classList.toggle('hidden');
    if (!submenu.classList.contains('.hidden')){
        for(let i=0; i< submenu_buttons.length; i++){
            let submenu_button = submenu_buttons[i];
            submenu_button.onclick = function () {
                title.innerHTML = submenu_button.innerHTML;
            }
        }
    }
}

//клик все блока dropdown
document.addEventListener('click', function(e) {
    const target = e.target;
    const its_dropdown = target === dropdown || dropdown.contains(target);
    let submenu_hidden = submenu.classList.contains('.hidden');

    if (!its_dropdown && !submenu_hidden) {
        submenu.classList.add('hidden');

    }

})



