using System;
using System.IO;

namespace Kaizen.Core.Pdf
{
    public interface IPdfGenerator
    {
        /**
         * Generate a certificate for the client.
         * <returns> a MemoryStream where the certificate is stored</returns>
         */
        MemoryStream GenerateCertificate(int certificateId, string tradeName, string nit, DateTime applicationDate,
            string serviceNames);
    }
}
