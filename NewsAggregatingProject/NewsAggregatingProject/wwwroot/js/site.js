
const domainAdress = window.location.origin;
function showLogoutModal() {

    const myModal = new bootstrap.Modal(document.getElementById('exampleModal'));
    myModal.toggle();

    myModal.show();
}

async function logout() {
    const testUrl = "/user/test";
    fetch(domainAdress + testUrl,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
        });
}