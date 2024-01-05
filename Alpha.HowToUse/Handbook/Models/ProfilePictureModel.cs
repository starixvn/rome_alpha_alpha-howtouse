using System;

namespace Alpha.HowToUse.Handbook.Models
{
    public class ProfilePictureModel
    {
        public ProfilePictureModel()
        {
        }

        public ProfilePictureModel(byte[] image, int imageId, DateTime? modified)
        {
            Image = image;
            ImageId = imageId;
            Modified = modified;
        }

        public byte[] Image { get; private set; }

        public int ImageId { get; private set; }

        public DateTime? Modified { get; private set; }

        public string ImageBase64String => Image == null ? string.Empty : $"data:image/jpeg;base64,{Convert.ToBase64String(Image)}";
    }
}