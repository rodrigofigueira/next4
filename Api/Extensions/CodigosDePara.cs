using System;

namespace Api.Extensions
{
    public static class CodigosDePara
    {
        public static Int32 ConverterCodigoPilarDeNegocios(this string valor)
        {

            if (valor.ToLower().Contains("mais de uma solução")) return 419500000;
            if (valor.ToLower().Contains("impressoras")) return 100000000;
            if (valor.ToLower().Contains("computadores")) return 100000001;
            if (valor.ToLower().Contains("mobile")) return 100000002;
            if (valor.ToLower().Contains("inovação digital")) return 100000003;
            if (valor.ToLower().Contains("outros")) return 100000004;

            return 0;

        }

        public static Int32 ConverterCodigoVolumeImpressao(this string valor)
        {
            if (valor.ToLower().Contains("abaixo de 79 mil")) return 100000000;
            if (valor.ToLower().Contains("entre 80 a 199 mil")) return 100000001;
            if (valor.ToLower().Contains("entre 200 a 499 mil")) return 100000002;
            if (valor.ToLower().Contains("entre 500 a 999 mil")) return 100000003;
            if (valor.ToLower().Contains("acima de 1 milhão")) return 100000004;
            if (valor.ToLower().Contains("não se aplica")) return 100000005;

            return 0;
        }

        public static Int32 ConvertCodigoQtdImpressorasMultiFuncionais(this string valor)
        {
            if (valor.ToLower().Contains("abaixo de 19")) return 100000000;
            if (valor.ToLower().Contains("entre 20 e 49")) return 100000001;
            if (valor.ToLower().Contains("entre 50 e 124")) return 100000002;
            if (valor.ToLower().Contains("entre 125 a 249")) return 100000003;
            if (valor.ToLower().Contains("acima de 250")) return 100000004;

            return 0;
        }

    }
}
