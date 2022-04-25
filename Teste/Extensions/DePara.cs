using Api.Extensions;
using Xunit;

namespace Teste.Extensions
{

    public class DePara
    {

        [Fact]
        public void PilarDeNegocios()
        {
            Assert.Equal(419500000, "mais de uma solução".ConverterCodigoPilarDeNegocios());
            Assert.Equal(100000000, "IMPRESSORAS".ConverterCodigoPilarDeNegocios());
            Assert.Equal(100000001, "cOMPUTAdoRES".ConverterCodigoPilarDeNegocios());
            Assert.Equal(100000002, "MOBILE".ConverterCodigoPilarDeNegocios());
            Assert.Equal(100000003, " INOVAÇÃO DIGITAL   ".ConverterCodigoPilarDeNegocios());
            Assert.Equal(100000004, "OUTROS".ConverterCodigoPilarDeNegocios());
        }

        [Fact]
        public void VolumeDeImpressao()
        {
            Assert.Equal(100000000, "Abaixo de 79 mil".ConverterCodigoVolumeImpressao());
            Assert.Equal(100000001, "Entre 80 a 199 mil".ConverterCodigoVolumeImpressao());
            Assert.Equal(100000002, "Entre 200 a 499 mil".ConverterCodigoVolumeImpressao());
            Assert.Equal(100000003, "Entre 500 a 999 mil".ConverterCodigoVolumeImpressao());
            Assert.Equal(100000004, "Acima de 1 milhão".ConverterCodigoVolumeImpressao());
            Assert.Equal(100000005, "Não se aplica".ConverterCodigoVolumeImpressao());
        }

        [Fact]
        public void QtdImpressorasMultiFuncionais()
        {
            Assert.Equal(100000000, "abaixo de 19".ConvertCodigoQtdImpressorasMultiFuncionais());
            Assert.Equal(100000001, "entre 20 e 49".ConvertCodigoQtdImpressorasMultiFuncionais());
            Assert.Equal(100000002, "entre 50 e 124".ConvertCodigoQtdImpressorasMultiFuncionais());
            Assert.Equal(100000003, "entre 125 a 249".ConvertCodigoQtdImpressorasMultiFuncionais());
            Assert.Equal(100000004, "acima de 250".ConvertCodigoQtdImpressorasMultiFuncionais());
        }


    }
}
