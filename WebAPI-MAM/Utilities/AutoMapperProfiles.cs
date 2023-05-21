﻿using AutoMapper;
using WebAPI_MAM.DTO_s.Get;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.DTO_s.Update;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //DTO Set

            CreateMap<PatientDTO, Patients>();
            CreateMap<MedicInfoDTO, MedicInfo>();
            CreateMap<DoctorDTO, Doctors>();
            CreateMap<Doctors, DoctorDTO>();
            CreateMap<AptmDTO, Appointments>();
            CreateMap<DiagnosisDTO, Diagnosis>();

            //Update
            CreateMap<UpPatientDTO, Patients>();
           // CreateMap<UpDoctorDTO, Doctors>().ReverseMap();

            //DTO Get

            CreateMap<Patients, GetPatientDTO>()
                .ForMember(dest => dest.MedicInfo, opt => opt.MapFrom(src => src.medicInfo))
                .ForMember(dest => dest.appointments, opt => opt.MapFrom(src => src.appointments));
            CreateMap<MedicInfo, GetMedicInfoDTO>();
            CreateMap<Appointments, GetAptmDTO>()
                .ForMember(dest => dest.diagnostic, opt => opt.MapFrom(src => src.diagnostic));

            CreateMap<Doctors, DoctorsDTOconCitas>()
                .ForMember(dest => dest.appointments, opt => opt.MapFrom(MapDoctorDTOAptms));

            //Others
            CreateMap<Patients, PatientDTOwithMedicInfo>().ForMember(pDTO => pDTO.medicInfoDTO, opt => opt.MapFrom(MapPatientDTOMedicInfo));
            CreateMap<Patients, PatientDTOconCitas>().ForMember(pDTO => pDTO.appointments, opt => opt.MapFrom(MapPatientDTOAptms));

            CreateMap<Doctors, GetDoctorDTO>();
            //CreateMap<Doctors, DoctorsDTOconCitas>().ForMember(dDTO => dDTO.appointments, opt => opt.MapFrom(MapDoctorDTOAptms));

            CreateMap<Appointments, AptmDTOwithDiag>().ForMember(aDTO => aDTO.diagnostic, opt => opt.MapFrom(MapAptmDTOwDiag));

        }

        #region PatientMapping
        private GetMedicInfoDTO MapPatientDTOMedicInfo(Patients patient, GetPatientDTO getPatientDTO)
        {
            if (patient.medicInfo == null) { return null; }

            var result = new GetMedicInfoDTO()
            {
                Id = patient.medicInfo.Id,
                nss = patient.medicInfo.nss,
                weight = patient.medicInfo.weight,
                height = patient.medicInfo.height,
                sicknessHistory = patient.medicInfo.sicknessHistory
            };

            return result;
        }

        private List<GetAptmDTO> MapPatientDTOAptms(Patients patient, GetPatientDTO getPatientDTO)
        {
            var result = new List<GetAptmDTO>();

            if (patient.appointments == null) { return result; }

            foreach(var aptm in patient.appointments)
            {
                result.Add(new GetAptmDTO()
                {
                    Id = aptm.Id,
                    Date = aptm.Date,
                    Status = aptm.Status,
                    patientId = aptm.patientId,
                    patientName = aptm.patient.name,
                    doctorId = aptm.doctorId,
                    doctorName = aptm.doctor.Name
                });
            }
            return result;
        }

        #endregion
        
        private List<GetAptmDTO> MapDoctorDTOAptms(Doctors doctor, GetDoctorDTO getDoctorDTO)
        {
            var result = new List<GetAptmDTO>();

            if (doctor.appointments == null) { return result; }

            foreach (var aptm in doctor.appointments)
            {
                result.Add(new GetAptmDTO()
                {
                    Id = aptm.Id,
                    Date = aptm.Date.Date,
                    Status = aptm.Status,
                    patientId = aptm.patientId,
                    patientName = aptm.patient.name,
                    doctorId = aptm.doctorId,
                    doctorName = aptm.doctor.Name,
                    diagId = aptm.diagId

                });
            }
            return result;
        }

        private GetDiagDTO MapAptmDTOwDiag(Appointments aptm, GetAptmDTO getAptmDTO)
        {
            if(aptm.diagnostic == null) { return null; }

            var result = new GetDiagDTO()
            {
                Id = aptm.diagnostic.Id,
                observations = aptm.diagnostic.observations,
                diagnostic = aptm.diagnostic.diagnostic,
                treatment = aptm.diagnostic.treatment,
                drugs = aptm.diagnostic.drugs
            };

            return result;
        }

    }
}