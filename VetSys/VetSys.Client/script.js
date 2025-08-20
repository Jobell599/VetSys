const API_URL = "https://localhost:7050/api/Animals";
const CUSTOMER_API_URL = "https://localhost:7050/api/Customers";

// 🚀 Cargar todas las mascotas
async function loadAnimals() {
    try {
        const response = await fetch(API_URL);
        if (!response.ok) throw new Error("Error al cargar las mascotas");

        const animals = await response.json();
        const tableBody = document.getElementById("animalsList");
        tableBody.innerHTML = "";

        animals.forEach(animal => {
            const row = `
                <tr>
                    <td>${animal.id}</td>
                    <td>${animal.name}</td>
                    <td>${animal.kind}</td>
                    <td>${animal.race}</td>
                    <td>${animal.sex}</td>
                    <td>${animal.weight}</td>
                    <td>${animal.customerId}</td>
                    <td>
                        <button class="edit" onclick="location.href='edit-animal.html?id=${animal.id}'">✏️ Editar</button>
                        <button onclick="deleteAnimal(${animal.id})">🗑️ Eliminar</button>
                    </td>
                </tr>
            `;
            tableBody.innerHTML += row;
        });
    } catch (error) {
        console.error(error);
        alert("No se pudo cargar la lista de mascotas.");
    }
}

// 🚀 Cargar dueños en el select
async function loadCustomers() {
    try {
        const res = await fetch(CUSTOMER_API_URL);
        if (!res.ok) throw new Error("Error al cargar los dueños");

        const customers = await res.json();
        const select = document.getElementById("customerId");
        select.innerHTML = "<option value=''>--Selecciona un dueño--</option>";
        customers.forEach(c => {
            const option = document.createElement("option");
            option.value = c.id;
            option.textContent = c.name;
            select.appendChild(option);
        });
    } catch (error) {
        console.error(error);
        alert("No se pudieron cargar los dueños.");
    }
}

// 🚀 Registrar o actualizar mascota
document.getElementById("animalForm").addEventListener("submit", async function(e) {
    e.preventDefault();

    const animalId = parseInt(document.getElementById("animalId").value);
    const animalData = {
        name: document.getElementById("name").value,
        kind: document.getElementById("kind").value,
        race: document.getElementById("race").value,
        sex: document.getElementById("sex").value,
        birth: document.getElementById("birth").value,
        weight: parseInt(document.getElementById("weight").value),
        customerId: parseInt(document.getElementById("customerId").value)
    };

    if (!animalData.customerId) {
        alert("Debes seleccionar un dueño válido");
        return;
    }

    try {
        let response;
        if (animalId) {
            // Actualizar
            response = await fetch(`${API_URL}/${animalId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(animalData)
            });
        } else {
            // Crear nueva
            response = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(animalData)
            });
        }

        if (response.ok) {
            alert(`Mascota ${animalId ? "actualizada" : "registrada"} correctamente 🐾`);
            document.getElementById("animalForm").reset();
            document.getElementById("animalId").value = "";
            document.getElementById("submitBtn").textContent = "Registrar mascota";
            loadAnimals();
        } else {
            const errorData = await response.json();
            console.error(errorData);
            alert("Error al guardar la mascota");
        }
    } catch (error) {
        console.error(error);
        alert("Error al conectarse con el servidor");
    }
});

// 🚀 Eliminar mascota
async function deleteAnimal(id) {
    if (!confirm("¿Seguro que quieres eliminar esta mascota?")) return;

    try {
        const response = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
        if (response.ok) {
            alert("Mascota eliminada correctamente 🗑️");
            loadAnimals();
        } else {
            alert("Error al eliminar la mascota");
        }
    } catch (error) {
        console.error(error);
        alert("Error al conectarse con el servidor");
    }
}

// 📌 Inicializar
loadCustomers();
loadAnimals();
