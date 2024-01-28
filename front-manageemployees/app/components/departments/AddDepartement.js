"use client";

import { AiOutlinePlus } from "react-icons/ai";
import Modal from "../utils/Modal";
import { useState } from "react";
import { addDepartment } from "@/api/departmentApi";
import { useDepartmentContext } from "@/app/contexts/DepartmentContext";

const AddDepartement = () => {
    const { updateDepartments } = useDepartmentContext();
    const [modalOpen, setModalOpen] = useState(false);

    const [formNameValue, setFormNameValue] = useState("");
    const [formDescriptionValue, setFormDescriptionValue] = useState("");
    const [formAdresseValue, setFormAdresseValue] = useState("");

    const handleSubmitNewTodo = async (e) => {
        e.preventDefault();
        await addDepartment({
            name: formNameValue,
            description: formDescriptionValue,
            address: formAdresseValue,
        });

        setFormNameValue("");
        setFormDescriptionValue("");
        setFormAdresseValue("");

        setModalOpen(false);
        updateDepartments();
    };

    return (
        <div>
            <button
                onClick={() => setModalOpen(true)}
                className='btn btn-primary w-full'
            >
                Ajouter un departement <AiOutlinePlus className='ml-2' size={18} />
            </button>

            <Modal modalOpen={modalOpen} setModalOpen={setModalOpen}>
                <form onSubmit={handleSubmitNewTodo}>
                    <h3 className="font-bold text-lg">Ajouter le département</h3>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Nom</span>
                        </div>
                        <input
                            value={formNameValue}
                            onChange={(e) => setFormNameValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Description</span>
                        </div>
                        <input
                            value={formDescriptionValue}
                            onChange={(e) => setFormDescriptionValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full mb-8">
                        <div className="label">
                            <span className="label-text">Adresse</span>
                        </div>
                        <input
                            value={formAdresseValue}
                            onChange={(e) => setFormAdresseValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <button type="submit" className="btn w-full">
                        Ajouter
                    </button>
                </form>
            </Modal>
        </div>
    );
};

export default AddDepartement;