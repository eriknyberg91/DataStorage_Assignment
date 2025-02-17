import { useState, useEffect } from "react";
import { getProjects, deleteProject } from "../Services/ProjectService";

const ProjectList = () => {
    const [projects, setProjects] = useState([]);
    const [error, setError] = useState("");

    const fetchProjects = async () => {
        try {
            const data = await getProjects();
            setProjects(data);
        } catch (err) {
            setError("Failed to fetch projects");
            console.log(err);
        }
    };

    useEffect(() => {
        fetchProjects();
    }, []);

    const handleDelete = async (id) => {
        try {
            await deleteProject(id);
            setProjects(projects.filter((project) => project.id !== id)); // Remove from list immediately
        } catch (err) {
            setError("Failed to delete project");
            console.log(err);
        }
    };

    return (
        <div>
            <h2>Project List</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}
            <ul>
                {projects.map((project) => (
                    <li key={project.id}>
                        {project.title}{" "}
                        <button
                            onClick={() => handleDelete(project.id)}
                            style={{ color: "white", backgroundColor: "red", border: "none", padding: "5px 10px", cursor: "pointer" }}
                        >
                            DELETE
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProjectList;


