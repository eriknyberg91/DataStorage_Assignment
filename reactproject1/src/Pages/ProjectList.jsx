import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { getProjects, deleteProject } from "../services/ProjectService";

//All Frontend is created with the help of ChatGPT
const ProjectList = () => {
    const [projects, setProjects] = useState([]);

    useEffect(() => {
        loadProjects();
    }, []);

    const loadProjects = async () => {
        const data = await getProjects();
        setProjects(data);
    };

    const handleDelete = async (id) => {
        await deleteProject(id);
        loadProjects(); // Refresh list after deletion
    };

    return (
        <div>
            <h2>Project List</h2>
            <Link to="/projects/create">
                <button>Create Project</button>
            </Link>
            <ul>
                {projects.map((project) => (
                    <li key={project.id}>
                        {project.title}
                        <Link to={`/projects/edit/${project.id}`}>
                            <button>Edit</button>
                        </Link>
                        <button onClick={() => handleDelete(project.id)} style={{ color: "red" }}>
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProjectList;
