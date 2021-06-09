let search_form = document.querySelector('#search_form');
let search_button = document.querySelector('#search_button');

search_button.onclick = function (event) {
    search_form.classList.toggle('hidden');
    event.stopPropagation();
}

let input = search_form.querySelector("input");

//кнопка на форме (иконка ага)
//по идее поиск по клику на нее или на enter
let submit_button = search_form.querySelector("button");

search_form.addEventListener('input', function(e) {
    //let value = input.value;
    console.log(input.value);
})