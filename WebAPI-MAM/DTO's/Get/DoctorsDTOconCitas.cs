﻿using WebAPI_MAM.Entities;

namespace WebAPI_MAM.DTO_s.Get
{
    public class DoctorsDTOconCitas : GetDoctorDTO //Valores del doctor y la lista de citas que tiene 
    {
        public List<Appointments> appointments { get; set; }  
    }
}
