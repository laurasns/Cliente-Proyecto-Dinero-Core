using ProyectoCoreLauraSanNicolas.Models;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class CalculatorMapper
    {
        public static CalculatorDTO MapToDTO(Calculator calculator)
        {
            CalculatorDTO calculatorDTO = new CalculatorDTO();
            calculatorDTO.Id = calculator.Id;
            calculatorDTO.Result = calculator.Result;
            calculatorDTO.UserId = calculator.UserId;
            return calculatorDTO;
        }
        public static Calculator MapToModel(CalculatorDTO calculatorDto)
        {
            Calculator calculator = new Calculator();
            calculator.Id = calculatorDto.Id;
            calculator.Result = calculatorDto.Result;
            calculator.UserId = calculatorDto.UserId;
            return calculator;
        }
    }
}
