let phones = [];
let companies = [];
let operatingSystems = [];
let connection = null;
let phoneIDToUpdate = -1;

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

    connection.on("PhoneCreated", (user, message) => {
        getdata();
    });

    connection.on("PhoneDeleted", (user, message) => {
        getdata();
    });

    connection.on("PhoneUpdated", (user, message) => {
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
    await fetch('http://localhost:22184/phone')
        .then(x => x.json())
        .then(y => {
            phones = y
            console.log(phones)
            display();
        });
    await fetch('http://localhost:22184/company')
        .then(x => x.json())
        .then(y => {
            companies = y
            console.log(companies)
            display();
        });
    await fetch('http://localhost:22184/SmartPhoneOS')
        .then(x => x.json())
        .then(y => {
            operatingSystems = y
            console.log(operatingSystems)
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    phones.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>
            <td>${t.phoneID}</td>
            <td>${t.name}</td>
            <td>${t.series}</td>
            <td>${new Date(t.releaseDate).toDateString()}</td>
            <td>${t.dataInput}</td>
            <td>${t.batterySize}</td>
            <td>${t.wirelessCharging}</td>
            <td>${t.companyID}</td>
            <td>${t.smartPhoneOSID}</td>
            <td><button type="button" onclick="remove(${t.phoneID})">Delete</button>
            <button type="button" onclick="showupdate(${t.phoneID})">Update</button></td>
         </tr>`
            ;
    });
}

function create() {
    let name = document.getElementById('name').value;
    let series = document.getElementById('series').value;
    let releaseDate = document.getElementById('releasedate').value;
    let dataInput = document.getElementById('datainput').value;
    let batterySize = document.getElementById('batterysize').value;
    let wirelessCharging = Boolean(document.getElementById('wirelesscharging').checked);

    let smartPhoneOSID = -1;
    let companyID = -1;
    let is_okay = true

    let smartPhoneOS = operatingSystems.find(t => t['smartPhoneOSID'] == Number(document.getElementById('osid').value));
    if (smartPhoneOS != undefined) {
        smartPhoneOSID = smartPhoneOS["smartPhoneOSID"];
    }
    else {
        alert("Smart Phone OS ID does not exist");
        is_okay = false;
    }

    let company = companies.find(t => t['companyID'] == Number(document.getElementById('companyid').value));
    if (company != undefined) {
        companyID = company["companyID"];
    }
    else {
        alert("Company ID does not exist");
        is_okay = false;
    }

    if (is_okay) {
        fetch('http://localhost:22184/phone',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', },
                body: JSON.stringify
                    ({
                        name: name,
                        series: series,
                        releaseDate: releaseDate,
                        dataInput: dataInput,
                        batterySize: batterySize,
                        wirelessCharging: wirelessCharging,
                        smartPhoneOSID: smartPhoneOSID,
                        companyID: companyID,
                    })
            })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });
    }
}

function remove(id) {
    fetch('http://localhost:22184/phone' + '/' + id,
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
    phoneIDToUpdate = id;
    let phone = phones.find(t => t['phoneID'] == id);

    document.getElementById('create_button').style.display = 'none';
    document.getElementById('update_buttons').style.display = 'flex';

    document.getElementById('name').value = phone.name;
    document.getElementById('series').value = phone.series;
    document.getElementById('releasedate').value = new Date(phone.releaseDate).toISOString().substring(0, 10);
    document.getElementById('datainput').value = phone.dataInput;
    document.getElementById('batterysize').value = phone.batterySize;
    document.getElementById('wirelesscharging').checked = phone.wirelessCharging;
    document.getElementById('companyid').value = phone.smartPhoneOSID;
    document.getElementById('osid').value = phone.companyID;
}

function hideupdate() {
    document.getElementById('create_button').style.display = 'unset';
    document.getElementById('update_buttons').style.display = 'none';

    document.getElementById('name').value = "";
    document.getElementById('series').value = "";
    document.getElementById('releasedate').value = "";
    document.getElementById('datainput').value = "";
    document.getElementById('batterysize').value = "";
    document.getElementById('wirelesscharging').checked = false;
    document.getElementById('companyid').value = "";
    document.getElementById('osid').value = "";
}

function update() {
    let name = document.getElementById('name').value;
    let series = document.getElementById('series').value;
    let releaseDate = document.getElementById('releasedate').value;
    let dataInput = document.getElementById('datainput').value;
    let batterySize = document.getElementById('batterysize').value;
    let wirelessCharging = Boolean(document.getElementById('wirelesscharging').checked);

    let smartPhoneOSID = -1;
    let companyID = -1;
    let is_okay = true

    let smartPhoneOS = operatingSystems.find(t => t['smartPhoneOSID'] == Number(document.getElementById('osid').value));
    if (smartPhoneOS != undefined) {
        smartPhoneOSID = smartPhoneOS["smartPhoneOSID"];
    }
    else {
        alert("Smart Phone OS ID does not exist");
        is_okay = false;
    }

    let company = companies.find(t => t['companyID'] == Number(document.getElementById('companyid').value));
    if (company != undefined) {
        companyID = company["companyID"];
    }
    else {
        alert("Company ID does not exist");
        is_okay = false;
    }

    if (is_okay) {
        fetch('http://localhost:22184/phone',
            {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json', },
                body: JSON.stringify
                    ({
                        phoneID: phoneIDToUpdate,
                        name: name,
                        series: series,
                        releaseDate: releaseDate,
                        dataInput: dataInput,
                        batterySize: batterySize,
                        wirelessCharging: wirelessCharging,
                        smartPhoneOSID: smartPhoneOSID,
                        companyID: companyID,
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
}