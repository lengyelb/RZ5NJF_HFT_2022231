let companies = [];
let connection = null;
let companyIDToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:22184/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("Connected", (user, message) => {
        ;
    });

    connection.on("CompanyCreated", (user, message) => {
        getdata();
    });

    connection.on("CompanyDeleted", (user, message) => {
        getdata();
    });

    connection.on("CompanyUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:22184/company')
        .then(x => x.json())
        .then(y => {
            companies = y
            console.log(companies)
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('resultarea').innerHTML += 
        `<tr>
            <td>${t.companyID}</td>
            <td>${t.name}</td>
            <td>${t.ceo}</td>
            <td>${t.headquarters}</td>
            <td>${new Date(t.founded).toDateString()}</td>
            <td>${t.numberOfEmployees}</td>
            <td>${t.netWorth}</td>
            <td><button type="button" onclick="remove(${t.companyID})">Delete</button>
            <button type="button" onclick="showupdate(${t.companyID})">Update</button></td>
         </tr>`        
        ;});
}

function create() {
    let name = document.getElementById('name').value;
    let ceo = document.getElementById('ceo').value;
    let headquarters = document.getElementById('headquarters').value;
    let founded = document.getElementById('founded').value;
    let employees = document.getElementById('employees').value;
    let networth = document.getElementById('networth').value;
    fetch('http://localhost:22184/company',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify
                ({
                    name: name,
                    ceo: ceo,
                    headquarters: headquarters,
                    founded: founded,
                    numberOfEmployees: employees,
                    netWorth: networth,
                })
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:22184/company' + '/' + id,
        {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json', },
            body: null
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(id) {
    companyIDToUpdate = id;
    let company = companies.find(t => t['companyID'] == id);

    document.getElementById('create_button').style.display = 'none';
    document.getElementById('update_buttons').style.display = 'flex';

    document.getElementById('name').value = company.name;
    document.getElementById('ceo').value = company.ceo;
    document.getElementById('headquarters').value = company.headquarters;
    document.getElementById('founded').value = new Date(company.founded).toISOString().substring(0, 10);
    document.getElementById('employees').value = company.numberOfEmployees;
    document.getElementById('networth').value = company.netWorth;
}

function hideupdate() {
    document.getElementById('create_button').style.display = 'unset';
    document.getElementById('update_buttons').style.display = 'none';

    document.getElementById('name').value = "";
    document.getElementById('ceo').value = "";
    document.getElementById('headquarters').value = "";
    document.getElementById('founded').value = "";
    document.getElementById('employees').value = "";
    document.getElementById('networth').value = "";
}

function update() {
    let name = document.getElementById('name').value;
    let ceo = document.getElementById('ceo').value;
    let headquarters = document.getElementById('headquarters').value;
    let founded = document.getElementById('founded').value;
    let employees = document.getElementById('employees').value;
    let networth = document.getElementById('networth').value;
    fetch('http://localhost:22184/company',
        {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify
                ({
                    companyID: companyIDToUpdate,
                    name: name,
                    ceo: ceo,
                    headquarters: headquarters,
                    founded: founded,
                    numberOfEmployees: employees,
                    netWorth: networth,
                })
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    hideupdate()
}