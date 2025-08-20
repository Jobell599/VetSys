

const API_URL = "https://localhost:7050/api/Animals";

// 1️⃣ Leer el ID de la URL
const urlParams = new URLSearchParams(window.location.search);
const animalId = urlParams.get('id');

// 2️⃣ Obtener datos de la mascota
async function loadAnimal() {
    try {
        const response = await fetch(`${API_URL}/${animalId}`);
        if (!response.ok) throw new Error("No se pudo cargar la mascota");
        const animal = await response.json();

        document.getElementById("animalId").value = animal.id;
        document.getElementById("name").value = animal.name;
        document.getElementById("kind").value = animal.kind;
        document.getElementById("race").value = animal.race;
        document.getElementById("sex").value = animal.sex;
        document.getElementById("birth").value = animal.birth.split("T")[0];
        document.getElementById("weight").value = animal.weight;
        document.getElementById("customerId").value = animal.customerId;

    } catch (error) {
        console.error(error);
        alert("Error al cargar la mascota");
    }
}

// 3️⃣ Enviar formulario para actualizar
document.getElementById("editAnimalForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const animalData = {
        id: parseInt(document.getElementById("animalId").value),
        name: document.getElementById("name").value,
        kind: document.getElementById("kind").value,
        race: document.getElementById("race").value,
        sex: document.getElementById("sex").value,
        birth: document.getElementById("birth").value,
        weight: parseInt(document.getElementById("weight").value),
        customerId: parseInt(document.getElementById("customerId").value)
    };

    try {
        const response = await fetch(`${API_URL}/${animalId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(animalData)
        });

        if (response.ok) {
            alert("Mascota actualizada correctamente 🐾");
            window.location.href = "index.html"; // Volver al listado
        } else {
            alert("Error al actualizar la mascota");
        }
    } catch (error) {
        console.error(error);
        alert("Error al conectarse con el servidor");
    }
});

// Cargar la mascota al abrir la página
loadAnimal();
