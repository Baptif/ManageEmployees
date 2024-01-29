"use client";

import { AiOutlinePlus } from "react-icons/ai";
import Modal from "../utils/Modal";
import { useState } from "react";
import { addEmployee } from "@/api/employeeApi";
import { useEmployeeContext } from "@/app/contexts/EmployeeContext";

const AddEmployee = () => {
    const { updateEmployees } = useEmployeeContext();
    const [modalOpen, setModalOpen] = useState(false);

    const [formFirstNameValue, setFormFirstNameValue] = useState("");
    const [formLastNameValue, setFormLastNameValue] = useState("");
    const [formPositionValue, setFormPositionValue] = useState("");
    const [formBirthDateValue, setFormBirthDateValue] = useState("");
    const [formEmailValue, setFormEmailValue] = useState("");
    const [formPhoneNumberValue, setFormPhoneNumberValue] = useState("");

    const handleSubmitNewEmployee = async (e) => {
        e.preventDefault();
        await addEmployee({
            firstName: formFirstNameValue,
            lastName: formLastNameValue,
            position: formPositionValue,
            birthDate: formBirthDateValue,
            email: formEmailValue,
            phoneNumber: formPhoneNumberValue,
        });

        setFormFirstNameValue("");
        setFormLastNameValue("");
        setFormPositionValue("");
        setFormBirthDateValue("");
        setFormEmailValue("");
        setFormPhoneNumberValue("");

        setModalOpen(false);
        updateEmployees();
    };

    return (
        <div>
            <button
                onClick={() => setModalOpen(true)}
                className='btn btn-primary w-full'
            >
                Ajouter un employé <AiOutlinePlus className='ml-2' size={18} />
            </button>

            <Modal modalOpen={modalOpen} setModalOpen={setModalOpen}>
                <form onSubmit={handleSubmitNewEmployee}>
                    <h3 className="font-bold text-lg">Ajouter un employé</h3>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Prénom</span>
                        </div>
                        <input
                            value={formFirstNameValue}
                            onChange={(e) => setFormFirstNameValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Nom de famille</span>
                        </div>
                        <input
                            value={formLastNameValue}
                            onChange={(e) => setFormLastNameValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Poste</span>
                        </div>
                        <input
                            value={formPositionValue}
                            onChange={(e) => setFormPositionValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Date de naissance</span>
                        </div>
                        <input
                            value={formBirthDateValue}
                            onChange={(e) => setFormBirthDateValue(e.target.value)}
                            type="date"
                            placeholder="Sélectionner la date"
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Email</span>
                        </div>
                        <input
                            value={formEmailValue}
                            onChange={(e) => setFormEmailValue(e.target.value)}
                            type="email"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full mb-8">
                        <div className="label">
                            <span className="label-text">Numéro de téléphone</span>
                        </div>
                        <input
                            value={formPhoneNumberValue}
                            onChange={(e) => setFormPhoneNumberValue(e.target.value)}
                            type="tel"
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

export default AddEmployee;