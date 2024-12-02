window.onbackbutton = function (callback) {
    window.onpopstate = function (event) {
        if (confirm("Vous allez perdre votre progrès si vous retournez à la page de connexion \nÊtes-vous certain de vouloir retourner à la connexion ?")) {
            callback();
        } else {
            history.pushState(null, null, location.href); // Prevent back navigation
        }
    };
};
