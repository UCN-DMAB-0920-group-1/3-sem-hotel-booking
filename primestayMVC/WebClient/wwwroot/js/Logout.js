$(Document).ready(() => {
    $("#logout_btn").click(() => {
        document.cookie = 'CustomerId = ';
        document.cookie = 'jwt = ';
    })
})
