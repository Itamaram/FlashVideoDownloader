using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FlashVideoFiles
{
    [Serializable]
    [XmlRoot("manifest")]
    public class F4Manifest
    {
        [XmlElement("id")]
        public string ID { get; set; }

        [XmlElement("label")]
        public string Label { get; set; }

        [XmlElement("lang")]
        public string Language { get; set; }

        [XmlElement("duration")]
        public float Duration { get; set; }

        [XmlElement("startTime")]
        public DateTime StartTime { get; set; }

        [XmlElement("mimeType")]
        public string MimeType { get; set; }

        [XmlElement("streamType")]
        public string StreamType { get; set; }

        [XmlElement("deliveryType")]
        public string DeliveryType { get; set; }

        [XmlElement("baseURL")]
        public string BaseUrl { get; set; }

        [XmlElement("drmAdditionalHeader")]
        public List<ManifestDRMAdditionalHeader> DRMAdditionalHeader { get; set; }

        [XmlElement("bootstrapInfo")]
        public List<ManifestBootstrapInfo> BootstreapInfo { get; set; }

        [XmlElement("media")]
        public List<ManifestMedia> Media { get; set; }

        public static F4Manifest FromXmlString(string xmlString)
        {
            xmlString = System.Text.RegularExpressions.Regex.Replace(xmlString, "xmlns=\".*?\"", "");
            var s = new XmlSerializer(typeof(F4Manifest));
            using (TextReader reader = new StringReader(xmlString))
            {
                var manifest = (F4Manifest)s.Deserialize(reader);
                foreach (var media in manifest.Media)
                    media.ManifestBootstrapInfo = manifest.BootstreapInfo.Where(bootstrap => bootstrap.ID == media.BootstrapInfoID).FirstOrDefault();
                return manifest;
            }
        }
    }

    [Serializable]
    public class ManifestDRMAdditionalHeader
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlText]
        public string AdditionalHeader { get; set; }
    }

    [Serializable]
    public class ManifestBootstrapInfo
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("profile")]
        public string Profile { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlText]
        public string BootstrapInfo { get; set; }

        [XmlIgnore]
        public BootstrapInfoBox BootstrapInfoBox
        {
            get
            {
                if (BootstrapInfo == null)
                    return null;
                else
                    return BootstrapInfoBox.FromBase64String(BootstrapInfo);
            }
        }
    }

    [Serializable]
    public class ManifestMedia
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("bitrate")]
        public int BitRate { get; set; }

        [XmlAttribute("streamId")]
        public string StreamID { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("bootstrapInfoId")]
        public string BootstrapInfoID { get; set; }

        [XmlAttribute("drmAdditionalHeaderId")]
        public string DRMAdditionalHeaderID { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("alternate")]
        public string Alternate { get; set; }

        [XmlAttribute("label")]
        public string Label { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlElement("moov")]
        public string Moov { get; set; }

        [XmlElement("metadata")]
        public string Metadata { get; set; }

        [XmlIgnore]
        public ManifestBootstrapInfo ManifestBootstrapInfo { get; set; }
    }
}