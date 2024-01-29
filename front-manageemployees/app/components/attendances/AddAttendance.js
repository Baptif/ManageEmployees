"use client";

import { AiOutlinePlus } from "react-icons/ai";
import Modal from "../utils/Modal";
import { useState } from "react";
import { createAttendance } from "@/api/attendanceApi";
import { useAttendanceContext } from "@/app/contexts/AttendanceContext";

const AddAttendance = () => {
    const { updateAttendanceList, selectedEmployeeId } = useAttendanceContext();
    const [modalOpen, setModalOpen] = useState(false);

    const [formStartDate, setFormStartDate] = useState("");
    const [formEndDate, setFormEndDate] = useState("");

    const handleSubmitNewAttendance = async (e) => {
        e.preventDefault();

        try {
            await createAttendance({
                employeeId:selectedEmployeeId,
                startDate: formStartDate,
                endDate: formEndDate,
            });
            updateAttendanceList(selectedEmployeeId);
        } catch (error) {
            console.error('Error adding attendance:', error);
        }

        setFormStartDate("");
        setFormEndDate("");

        setModalOpen(false);
    };

    return (
        <div>
            <button
                onClick={() => setModalOpen(true)}
                className='btn btn-primary w-full'
                disabled={!selectedEmployeeId}
            >
                Ajouter une présence <AiOutlinePlus className='ml-2' size={18} />
            </button>

            <Modal modalOpen={modalOpen} setModalOpen={setModalOpen}>
                <form onSubmit={handleSubmitNewAttendance}>
                    <h3 className="font-bold text-lg">Ajouter une présence</h3>

                    <label className="form-control w-full">
                        <div className="label">
                            <span className="label-text">Date de début</span>
                        </div>
                        <input
                            value={formStartDate}
                            onChange={(e) => setFormStartDate(e.target.value)}
                            type="datetime-local"
                            className="input input-bordered w-full"
                        />
                    </label>

                    <label className="form-control w-full mb-4">
                        <div className="label">
                            <span className="label-text">Date de fin</span>
                        </div>
                        <input
                            value={formEndDate}
                            onChange={(e) => setFormEndDate(e.target.value)}
                            type="datetime-local"
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

export default AddAttendance;
