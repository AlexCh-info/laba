using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public class PhotoAlbum
    {
        private List<Photo> photos = new List<Photo>();
        private ListView listView;

        public PhotoAlbum(ListView listView)
        {
            this.listView = listView;
            LoadPhotos();
        }

        private void LoadPhotos()
        {
            listView.Items.Clear();
            foreach (var photo in photos)
            {
                listView.Items.Add(new ListViewItem(new[] { photo.Path, photo.Description, photo.DateTaken.ToString("dd.MM.yyyy") }));
            }
        }
        public void AddPhoto()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory =
    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Title = "Выберите фото";
                openFileDialog.Filter = "Изображения (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var photoPath = openFileDialog.FileName;
                    var description = GetDescription();
                    var dateTaken = DateTime.ParseExact(description, "dd.MM.yyyy", null);

                    photos.Add(new Photo(photoPath, description, dateTaken));
                    LoadPhotos();
                    MessageBox.Show("Фото добавлено.");
                }
            }
        }

        public void RemovePhoto()
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите фото для удаления.");
                return;
            }

            var photoPath = listView.SelectedItems[0].SubItems[0].Text;
            photos.RemoveAll(p => p.Path == photoPath);
            LoadPhotos();
            MessageBox.Show("Фото удалено.");
        }

        public void SortPhotosByDate()
        {
            var sortedPhotos = photos.OrderBy(p => p.DateTaken).ToList();
            photos = new List<Photo>(sortedPhotos);
            LoadPhotos();
            MessageBox.Show("Фото отсортированы по дате.");
        }

        private string GetDescription()
        {
            // Простая замена без создания отдельной формы
            var form = new Form();
            form.Text = "Описание";
            form.Size = new System.Drawing.Size(300, 120);
            form.StartPosition = FormStartPosition.CenterScreen;

            var textBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(260, 20)
            };

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(90, 40)
            };

            form.Controls.Add(textBox);
            form.Controls.Add(btnOk);
            form.AcceptButton = btnOk;

            string result = "Без описания";
            if (form.ShowDialog() == DialogResult.OK)
                result = textBox.Text;

            form.Dispose();
            return result;
        }
    }
}