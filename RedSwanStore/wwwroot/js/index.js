

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
let dropdowns = document.querySelectorAll('.dropdown');

dropdowns.forEach(function (dropdown) {
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
                    submenu.classList.toggle('hidden');
                }
            }
        }
    }
    //клик вне блока dropdown
    document.addEventListener('click', function(e) {
        const target = e.target;
        const its_dropdown = target === dropdown || dropdown.contains(target);
        let submenu_hidden = submenu.classList.contains('.hidden');

        if (!its_dropdown && !submenu_hidden) {
            submenu.classList.add('hidden');

        }

    })
})



/* ----- FILTERS AND SORTS FUNCTIONALITY ----- */

// the filters and sort chosen by user
class ControllerArgs {
    #collections;
    #successCallback;

    /**
     * Create a new instance of Controller Args with specified Game Cards Block and with
     * specified callback that will be called .
     * @param successCallback the callback called if HTML returned by the server is empty.
     */
    constructor(successCallback) {
        this.#collections = {
            "genre": [],
            "price": [],
            "feature": [],
            "sort": ["default",]
        }
        
        this.#successCallback = successCallback;
    }

    /**
     * Add new controller argument of specified type.
     * @param type the type of controller argument (got from 'data-type' attribute).
     * @param controllerArg the controller argument itself (got from 'data-controller-arg' attribute).
     */
    addArg(type, controllerArg) {
        if (type === "sort")
            this.#collections["sort"].pop();
        
        this.#collections[type].push(controllerArg);
    }

    /**
     * Remove controller argument of specified type.
     * @param type the type of controller argument (got from 'data-type' attribute).
     * @param controllerArg the controller argument itself (got from 'data-controller-arg' attribute).
     */
    removeArg(type, controllerArg) {
        for (let i = 0; i < this.#collections[type].length; i++) {
            if (this.#collections[type][i] === controllerArg) {
                this.#collections[type].splice(i, 1);
                break;
            }
        }
    }

    /**
     * Get all stored controller args collections.
     * @returns {*}
     */
    getCollections() {
        return this.#collections;
    }

    /**
     * Send all stored data to server using AJAX and replace received HTML in 
     * specified in constructor 'gamesCardsBlock' block.
     */
    sendDataToServer() {
        $.ajax({
            type: 'post',
            url: '/home/sort-and-filter',
            data: {
                genresIds: this.#collections["genre"],
                featuresIds: this.#collections["feature"],
                pricesIds: this.#collections["price"],
                sortsIds: this.#collections["sort"]
            },
            dataType: 'html',
            error: (message) => {
                alert(message.toString());
            },
            success: (html) => this.#successCallback(html)
        });
    }
}



// the ControllerArgs instance to store filters and sort chosen by user
let controllerArgs = new ControllerArgs(
    (html) => {
        let gamesCardsBlock = $('#games-cards');
       
        gamesCardsBlock.empty();
        gamesCardsBlock.append(html);
        let game_card = document.querySelector(".game-card");
        let message = document.querySelector(".section_empty");
        
       
        if (!game_card)
            message.classList.remove("hidden");
        
        else
            message.classList.add("hidden");
        
            
        
    }
);



// make <li> items behave as checkboxes, save chosen filters and send data to server
let filtersItems = document.querySelectorAll('.filter .options__item li');

filtersItems.forEach((li) => {
    li.addEventListener('click', function(e) {
        let type = li.getAttribute("data-type");
        let arg = li.getAttribute("data-controller-arg");
        
        let isChecked = li.getAttribute("data-checked") === "true";
        
        if (isChecked) {
            li.setAttribute("data-checked", "false");
            li.classList.remove("filter__item_checked");
            
            controllerArgs.removeArg(type, arg);
        }
        else {
            li.setAttribute("data-checked", "true");
            li.classList.add("filter__item_checked");
            
            controllerArgs.addArg(type, arg);
        }

        controllerArgs.sendDataToServer();
    })
});


// make sort buttons save chosen sort type and send data to server
let sortButtons = document.querySelectorAll(".sort-button");

sortButtons.forEach((btn) => {
    btn.addEventListener('click', function(e) {
        let type = btn.getAttribute("data-type");
        let arg = btn.getAttribute("data-controller-arg");
        
        controllerArgs.addArg(type, arg);
        controllerArgs.sendDataToServer();
    })
});



