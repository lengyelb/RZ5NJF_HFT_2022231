let operatingSystems = [];
let connection = null;
let operatingSystemIDToUpdate = -1;

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

    connection.on("SmartPhoneOSCreated", (user, message) => {
        getdata();
    });

    connection.on("SmartPhoneOSDeleted", (user, message) => {
        getdata();
    });

    connection.on("SmartPhoneOSUpdated", (user, message) => {
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
    operatingSystems.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr>
            <td>${t.smartPhoneOSID}</td>
            <td>${t.name}</td>
            <td>${t.osFamily}</td>
            <td>${t.kernel}</td>
            <td>${t.packageManager}</td>
            <td>${new Date(t.releaseDate).toDateString()}</td>
            <td>${t.isSupported}</td>
            <td><button type="button" onclick="remove(${t.smartPhoneOSID})">Delete</button>
            <button type="button" onclick="showupdate(${t.smartPhoneOSID})">Update</button></td>
         </tr>`
            ;
    });
}

function create() {
    let name = document.getElementById('name').value;
    let osFamily = document.getElementById('osfamily').value;
    let kernel = document.getElementById('kernel').value;
    let packageManager = document.getElementById('packagemanager').value;
    let releaseDate = document.getElementById('releasedate').value;
    let isSupported = document.getElementById('issupported').checked;
    fetch('http://localhost:22184/SmartPhoneOS',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify
                ({
                    name: name,
                    osFamily: osFamily,
                    kernel: kernel,
                    packageManager: packageManager,
                    releaseDate: releaseDate,
                    isSupported: isSupported,
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
    fetch('http://localhost:22184/SmartPhoneOS' + '/' + id,
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
    operatingSystemIDToUpdate = id;
    let operatingSystem = operatingSystems.find(t => t['smartPhoneOSID'] == id);

    document.getElementById('create_button').style.display = 'none';
    document.getElementById('update_buttons').style.display = 'flex';

    document.getElementById('name').value = operatingSystem.name;
    document.getElementById('osfamily').value = operatingSystem.osFamily;
    document.getElementById('kernel').value = operatingSystem.kernel;
    document.getElementById('packagemanager').value = operatingSystem.packageManager;
    document.getElementById('releasedate').value = new Date(operatingSystem.releaseDate).toISOString().substring(0, 10);
    document.getElementById('issupported').checked = operatingSystem.isSupported;
}

function hideupdate() {
    document.getElementById('create_button').style.display = 'unset';
    document.getElementById('update_buttons').style.display = 'none';

    document.getElementById('name').value = "";
    document.getElementById('osfamily').value = "";
    document.getElementById('kernel').value = "";
    document.getElementById('packagemanager').value = "";
    document.getElementById('releasedate').value = "";
    document.getElementById('issupported').checked = false;
}

function update() {
    let name = document.getElementById('name').value;
    let osFamily = document.getElementById('osfamily').value;
    let kernel = document.getElementById('kernel').value;
    let packageManager = document.getElementById('packagemanager').value;
    let releaseDate = document.getElementById('releasedate').value;
    let isSupported = document.getElementById('issupported').checked;
    fetch('http://localhost:22184/SmartPhoneOS',
        {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify
                ({
                    smartPhoneOSID: operatingSystemIDToUpdate,
                    name: name,
                    osFamily: osFamily,
                    kernel: kernel,
                    packageManager: packageManager,
                    packageManager: packageManager,
                    releaseDate: releaseDate,
                    isSupported: isSupported,
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