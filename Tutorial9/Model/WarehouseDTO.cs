﻿using System.ComponentModel.DataAnnotations;

namespace Tutorial9.Model;

public class WarehouseDTO
{
    public int IdWarehouse { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
}