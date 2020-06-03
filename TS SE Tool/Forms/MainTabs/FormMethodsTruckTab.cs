/*
   Copyright 2016-2020 LIPtoH <liptoh.codebase@gmail.com>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TS_SE_Tool
{
    public partial class FormMain
    {
        //User Trucks tab
        private void CreateTruckPanelControls()
        {
            CreateTruckPanelButtons();
            CreateTruckPanelProgressBars();
        }

        private void CreateTruckPanelProgressBars()
        {
            int pHeight = RepairImg.Height, pOffset = 5, lOffset = 60, pSizeW = 300;
            int pSkillsNameHeight = 32, pSkillsNameWidth = 32, pSkillsNameOffset = 5, pSkillsNamelOffset = 12;

            string[] toolskillimgtooltip = new string[] { "Engine", "Transmission", "Chassis", "Cabin", "Wheels" };
            Label slabel, labelpartName;
            Panel Ppanel;

            for (int i = 0; i < 5; i++)
            {
                slabel = new Label();
                groupBoxUserTruckTruckDetails.Controls.Add(slabel);
                slabel.Name = "labelTruckPartName" + toolskillimgtooltip[i];
                slabel.Location = new Point(pSkillsNamelOffset, 23 + (pSkillsNameHeight + pSkillsNameOffset * 3) * i);
                slabel.Text = toolskillimgtooltip[i];
                slabel.AutoSize = true;

                labelpartName = new Label();
                groupBoxUserTruckTruckDetails.Controls.Add(labelpartName);
                labelpartName.Name = "labelTruckPartDataName" + i;
                labelpartName.Location = new Point(lOffset + pSizeW / 2, 23 + (pSkillsNameHeight + pSkillsNameOffset * 3) * i);
                labelpartName.Text = "";
                labelpartName.AutoSize = true;

                Panel imgpanel = new Panel();
                groupBoxUserTruckTruckDetails.Controls.Add(imgpanel);

                imgpanel.Parent = groupBoxUserTruckTruckDetails;
                imgpanel.Location = new Point(pSkillsNamelOffset, 23 + 14 + (pSkillsNameHeight + pSkillsNameOffset * 3) * i);
                imgpanel.BorderStyle = BorderStyle.None;
                imgpanel.Size = new Size(pSkillsNameWidth, pSkillsNameHeight);
                imgpanel.Name = "TruckPartImg" + i.ToString();

                Bitmap bgimg = new Bitmap(TruckPartsImg[i], pSkillsNameHeight, pSkillsNameWidth);
                imgpanel.BackgroundImage = bgimg;

                //Panel 
                Ppanel = new Panel();
                groupBoxUserTruckTruckDetails.Controls.Add(Ppanel);

                Ppanel.Parent = groupBoxUserTruckTruckDetails;
                Ppanel.Location = new Point(lOffset, imgpanel.Location.Y + pOffset);
                Ppanel.BorderStyle = BorderStyle.FixedSingle;
                Ppanel.Size = new Size(pSizeW, 24);
                Ppanel.Name = "progressbarTruckPart" + i.ToString();

                Button button = new Button();
                groupBoxUserTruckTruckDetails.Controls.Add(button);

                button.Parent = groupBoxUserTruckTruckDetails;
                button.Location = new Point(Ppanel.Location.X + Ppanel.Width + pOffset, imgpanel.Location.Y);
                button.FlatStyle = FlatStyle.Flat;
                button.Size = new Size(RepairImg.Height, RepairImg.Height);
                button.Name = "buttonTruckElRepair" + i.ToString();
                button.BackgroundImage = RepairImg;
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.Text = "";
                button.FlatAppearance.BorderSize = 0;
                button.Click += new EventHandler(buttonElRepair_Click);
            }

            Panel Ppanelf = new Panel();
            groupBoxUserTruckTruckDetails.Controls.Add(Ppanelf);
            Ppanelf.Parent = groupBoxUserTruckTruckDetails;
            Ppanelf.Location = new Point(lOffset + pSizeW + pOffset * 2 + RepairImg.Width, 23 + 14);
            Ppanelf.BorderStyle = BorderStyle.FixedSingle;
            Ppanelf.Size = new Size(50, 220);
            Ppanelf.Name = "progressbarTruckFuel";

            slabel = new Label();
            groupBoxUserTruckTruckDetails.Controls.Add(slabel);
            slabel.Name = "labelTruckDetailsFuel";
            slabel.Text = "Fuel";
            slabel.AutoSize = true;
            slabel.Location = new Point(Ppanelf.Location.X + (Ppanelf.Width - slabel.Width) / 2, Ppanelf.Location.Y + Ppanelf.Height + 10);
        }

        private void CreateTruckPanelButtons()
        {
            int pHeight = RepairImg.Height, pOffset = 5, tOffset = comboBoxUserTruckCompanyTrucks.Location.Y;
            int topbutoffset = comboBoxUserTruckCompanyTrucks.Location.X + comboBoxUserTruckCompanyTrucks.Width + pOffset;

            Button buttonInfo = new Button();
            tableLayoutPanel8.Controls.Add(buttonInfo, 0, 1);
            buttonInfo.FlatStyle = FlatStyle.Flat;
            buttonInfo.Size = new Size(CutomizeImg.Width, CutomizeImg.Height);
            buttonInfo.Name = "buttonTruckInfo";
            buttonInfo.BackgroundImage = ConvertBitmapToGrayscale(CutomizeImg);
            buttonInfo.BackgroundImageLayout = ImageLayout.Zoom;
            buttonInfo.Text = "";
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Enabled = false;
            buttonInfo.Dock = DockStyle.Fill;

            Button buttonR = new Button();
            tableLayoutPanel8.Controls.Add(buttonR, 3, 1);
            buttonR.FlatStyle = FlatStyle.Flat;
            buttonR.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonR.Name = "buttonTruckRepair";
            buttonR.BackgroundImage = RepairImg;
            buttonR.BackgroundImageLayout = ImageLayout.Zoom;
            buttonR.Text = "";
            buttonR.FlatAppearance.BorderSize = 0;
            buttonR.Click += new EventHandler(buttonTruckRepair_Click);
            buttonR.Dock = DockStyle.Fill;

            Button buttonF = new Button();
            tableLayoutPanel8.Controls.Add(buttonF, 4, 1);
            buttonF.FlatStyle = FlatStyle.Flat;
            buttonF.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonF.Name = "buttonTruckReFuel";
            buttonF.BackgroundImage = RefuelImg;
            buttonF.BackgroundImageLayout = ImageLayout.Zoom;
            buttonF.Text = "";
            buttonF.FlatAppearance.BorderSize = 0;
            buttonF.Click += new EventHandler(buttonTruckReFuel_Click);
            buttonF.Dock = DockStyle.Fill;

        }

        private void FillUserCompanyTrucksList()
        {
            DataTable combDT = new DataTable();
            DataColumn dc = new DataColumn("UserTruckNameless", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("UserTruckName", typeof(string));
            combDT.Columns.Add(dc);

            foreach (KeyValuePair<string, UserCompanyTruckData> UserTruck in UserTruckDictionary)
            {
                string truckname = "";
                try
                {
                    string templine = UserTruck.Value.Parts.Find(x => x.PartType == "truckbrandname").PartData.Find(xline => xline.StartsWith(" data_path:"));
                    truckname = templine.Split(new char[] { '"' })[1].Split(new char[] { '/' })[4];
                }
                catch { }
                TruckBrandsLngDict.TryGetValue(truckname, out string trucknamevalue);

                string TruckName = "";

                if (UserTruckDictionary[UserTruck.Key].Users)
                    TruckName = "[U] ";
                else
                    TruckName = "[Q] ";

                if (trucknamevalue != null && trucknamevalue != "")
                {
                    TruckName += trucknamevalue;
                    combDT.Rows.Add(UserTruck.Key, TruckName);
                }
                else
                {
                    TruckName += truckname;
                    combDT.Rows.Add(UserTruck.Key, TruckName);
                }
            }
            //combDT.DefaultView.Sort = "UserTruckName ASC";
            comboBoxUserTruckCompanyTrucks.ValueMember = "UserTruckNameless";
            comboBoxUserTruckCompanyTrucks.DisplayMember = "UserTruckName";
            comboBoxUserTruckCompanyTrucks.DataSource = combDT;
            comboBoxUserTruckCompanyTrucks.SelectedValue = PlayerDataV.UserCompanyAssignedTruck;
        }

        private void UpdateTruckPanelProgressBars()
        {
            UserTruckDictionary.TryGetValue(comboBoxUserTruckCompanyTrucks.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTruck);

            for (int i = 0; i < 5; i++)
            {
                Panel pnl = null;
                Label labelPart = null;

                string pnlname = "progressbarTruckPart" + i.ToString(), labelPartName = "labelTruckPartDataName" + i.ToString();

                if (groupBoxUserTruckTruckDetails.Controls.ContainsKey(pnlname))
                {
                    pnl = groupBoxUserTruckTruckDetails.Controls[pnlname] as Panel;
                }

                if (groupBoxUserTruckTruckDetails.Controls.ContainsKey(labelPartName))
                {
                    labelPart = groupBoxUserTruckTruckDetails.Controls[labelPartName] as Label;
                }

                if (pnl != null)
                {
                    List<string> TruckDataPart = null;

                    switch (i)
                    {
                        case 0:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "engine").PartData;
                            break;
                        case 1:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "transmission").PartData;
                            break;
                        case 2:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "chassis").PartData;
                            break;
                        case 3:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "cabin").PartData;
                            break;
                        case 4:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "tire").PartData;
                            break;
                    }
                    string wear = "0";
                    if (TruckDataPart != null)
                    {
                        if (labelPart != null)
                        {
                            labelPart.Text = TruckDataPart.Find(xl => xl.StartsWith(" data_path:")).Split(new char[] { '"' })[1].Split(new char[] { '/' }).Last().Split(new char[] { '.' })[0];
                        }

                        wear = TruckDataPart.Find(xl => xl.StartsWith(" wear:")).Split(new char[] { ' ' })[2];
                    }
                    else
                    {
                        labelPart.Text = "!! Part not found !!";
                    }

                    labelPart.Location = new Point(pnl.Location.X + (pnl.Width - labelPart.Width) / 2, labelPart.Location.Y);

                    decimal _wear = 0;

                    if (wear != "0" && wear != "1")
                        _wear = Utilities.NumericUtilities.HexFloatToDecimalFloat(wear);
                    else
                    if (wear == "1")
                        _wear = 1;

                    SolidBrush ppen = new SolidBrush(GetProgressbarColor(_wear));

                    int x = 0, y = 0, pnlwidth = (int)(pnl.Width * (1 - _wear));

                    Bitmap progress = new Bitmap(pnl.Width, pnl.Height);

                    Graphics g = Graphics.FromImage(progress);
                    g.FillRectangle(ppen, x, y, pnlwidth, pnl.Height);

                    int fontSize = 12;
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    GraphicsPath p = new GraphicsPath();
                    p.AddString(
                        ((int)((1 - _wear) * 100)).ToString() + " %",   // text to draw
                        FontFamily.GenericSansSerif,                    // or any other font family
                        (int)FontStyle.Bold,                            // font style (bold, italic, etc.)
                        g.DpiY * fontSize / 72,                         // em size
                        new Rectangle(0, 0, pnl.Width, pnl.Height),     // location where to draw text
                        sf);                                            // set options here (e.g. center alignment)
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillPath(Brushes.Black, p);
                    g.DrawPath(Pens.Black, p);

                    pnl.BackgroundImage = progress;
                }
            }

            Panel pnlfuel = null;
            string pnlnamefuel = "progressbarTruckFuel";
            if (groupBoxUserTruckTruckDetails.Controls.ContainsKey(pnlnamefuel))
            {
                pnlfuel = groupBoxUserTruckTruckDetails.Controls[pnlnamefuel] as Panel;
            }

            if (pnlfuel != null)
            {
                string fuel = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "truckdata").PartData.Find(xl => xl.StartsWith(" fuel_relative:")).Split(new char[] { ' ' })[2];//SelectedUserCompanyTruck.Fuel;
                decimal _fuel = 0;

                if (fuel != "0" && fuel != "1")
                    _fuel = Utilities.NumericUtilities.HexFloatToDecimalFloat(fuel);
                else
                if (fuel == "1")
                    _fuel = 1;

                SolidBrush ppen = new SolidBrush(GetProgressbarColor(1 - _fuel));
                int pnlheight = (int)(pnlfuel.Height * (_fuel)), x = 0, y = pnlfuel.Height - pnlheight;

                Bitmap progress = new Bitmap(pnlfuel.Width, pnlfuel.Height);

                Graphics g = Graphics.FromImage(progress);
                g.FillRectangle(ppen, x, y, pnlfuel.Width, pnlheight);

                int fontSize = 10;
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    ((int)(_fuel * 100)).ToString() + " %",             // text to draw
                    FontFamily.GenericSansSerif,                        // or any other font family
                    (int)FontStyle.Regular,                             // font style (bold, italic, etc.)
                    g.DpiY * fontSize / 72,                             // em size
                    new Rectangle(0, 0, pnlfuel.Width, pnlfuel.Height), // location where to draw text
                    sf);                                                // set options here (e.g. center alignment)
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(Brushes.Black, p);
                g.DrawPath(Pens.Black, p);

                pnlfuel.BackgroundImage = progress;
            }

            string lctxt = "";
            labelLicensePlate.Text = "";
            int stindex = 0, endindex = 0;

            string LicensePlate = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "truckdata").PartData.Find(xl => xl.StartsWith(" license_plate:")).Split(new char[] { '"' })[1];

            for (int i = 0; i < LicensePlate.Length; i++)
            {
                if (LicensePlate[i] == '<')
                {
                    endindex = i;
                    lctxt += LicensePlate.Substring(stindex, endindex - stindex);
                }
                else if (LicensePlate[i] == '>')
                {
                    stindex = i + 1;
                }
                else if (i == LicensePlate.Length - 1)
                {
                    endindex = i + 1;
                    lctxt += LicensePlate.Substring(stindex, endindex - stindex);
                }
            }
            if (lctxt.Split(new char[] { '|' }).Length > 1)
                labelLicensePlate.Text = lctxt.Split(new char[] { '|' })[0] + " Country " + lctxt.Split(new char[] { '|' })[1];
            else
                labelLicensePlate.Text = lctxt.Split(new char[] { '|' })[0];
        }

        //Events
        private void comboBoxCompanyTrucks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbbx = sender as ComboBox;

            if (cmbbx.SelectedIndex != -1)
            {
                UpdateTruckPanelProgressBars();
            }
        }

        public void buttonTruckReFuel_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (string temp in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckdata").PartData)
            {
                if (temp.StartsWith(" fuel_relative:"))
                {
                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckdata").PartData[i] = " fuel_relative: 1";
                    break;
                }
                i++;
            }
            UpdateTruckPanelProgressBars();
        }

        public void buttonTruckRepair_Click(object sender, EventArgs e)
        {
            string[] PartList = { "engine", "transmission", "chassis", "cabin", "tire" };

            foreach (string tempPart in PartList)
            {
                foreach (UserCompanyTruckDataPart temp in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.FindAll(x => x.PartType == tempPart))
                {
                    string partNameless = temp.PartNameless;

                    int i = 0;

                    foreach (string temp2 in temp.PartData)
                    {
                        if (temp2.StartsWith(" wear:"))
                        {
                            UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                            break;
                        }
                        i++;
                    }
                }
            }

            UpdateTruckPanelProgressBars();
        }

        public void buttonElRepair_Click(object sender, EventArgs e)
        {
            Button curbtn = sender as Button;
            int bi = Convert.ToByte(curbtn.Name.Substring(19));

            string[] PartList = { "engine", "transmission", "chassis", "cabin", "tire" };

            foreach (UserCompanyTruckDataPart temp in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.FindAll(x => x.PartType == PartList[bi]))
            {
                string partNameless = temp.PartNameless;

                int i = 0;

                foreach (string temp2 in temp.PartData)
                {
                    if (temp2.StartsWith(" wear:"))
                    {
                        UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                        break;
                    }
                    i++;
                }
            }

            UpdateTruckPanelProgressBars();
        }

        private void buttonUserTruckSelectCurrent_Click(object sender, EventArgs e)
        {
            comboBoxUserTruckCompanyTrucks.SelectedValue = PlayerDataV.UserCompanyAssignedTruck;
        }

        private void buttonUserTruckSwitchCurrent_Click(object sender, EventArgs e)
        {
            PlayerDataV.UserCompanyAssignedTruck = comboBoxUserTruckCompanyTrucks.SelectedValue.ToString();
        }

        //Share buttons
        private void buttonTruckPaintCopy_Click(object sender, EventArgs e)
        {
            string tempPaint = "TruckPaint\r\n";

            List<string> paintstr = UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData;

            foreach (string temp in paintstr)
            {
                tempPaint += temp + "\r\n";
            }

            string asd = BitConverter.ToString(Utilities.ZipDataUtilitiescs.zipText(tempPaint)).Replace("-", "");
            Clipboard.SetText(asd);
            MessageBox.Show("Paint data has been copied.");
        }

        private void buttonTruckPaintPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string inputData = Utilities.ZipDataUtilitiescs.unzipText(Clipboard.GetText());
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TruckPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

                    MessageBox.Show("Paint data  has been inserted.");
                }
                else
                    MessageBox.Show("Wrong data. Expected Paint data but\r\n" + Lines[0] + "\r\nwas found.");
            }
            catch
            {
                MessageBox.Show("Something gone wrong with decoding.");
            }
        }

        private void buttonTruckCapricornsPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string truckname = "";
                try
                {
                    string templine = UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckbrandname").PartData.Find(xline => xline.StartsWith(" data_path:"));
                    truckname = templine.Split(new char[] { '"' })[1].Split(new char[] { '/' })[4];
                }
                catch { }
                TruckBrandsLngDict.TryGetValue(truckname, out string trucknamevalue);

                string TruckName = "";

                if (trucknamevalue != null && trucknamevalue != "")
                {
                    TruckName += trucknamevalue;
                }
                else
                {
                    TruckName += truckname;
                }

                string decode = "";
                if (TruckName.Equals("MAN TGX"))
                {
                    decode = "1F8B0800A6CC995E00FF5590CB6AC3301045F781FC83C8A2B41022C9D1335FD145F762248D13D5766C6CF541BFBE96D387B3BB0CE70E87FB32BE85E619D2356F37A483A971A30B7DDB8F27F2F870AC75652A2FF6648E2C4A065589D1EA3A0AF3F4DB38AF1A511A2314EC092B2432292AC3FE48BF22BD50E815FF2123D7DC2A59C8BA850657201A258495CB3F691447BB14400B79B4B7421AEEA4BD16212ED21251627DF38728010BEF61FAFFCF168145F10361BEB03945C8E006C89713D9D188357DC74B0A2DD25CE6A21D5C0FF9FC4987B29B7BED3DFD4A1D70D7810B8729A5DD76F30DD14F5F1558010000";
                }
                else if (TruckName.Equals("MAN TGX Euro 6"))
                {
                    decode = "1F8B0800CECC995E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B34C09050DBB16593B6EAD7D7903EDCDDD5E8DCD1D17D996EAE7D86784DDB0DE9616ECD64DCD00DD3893C3E1C4353ABDAF23D5922F382419DA3D74DF05C3DFD34CEAB86174A71097BC232894CF05AB15FD2AE48CB255A597D93BE6A2A2D452643072DAE405492732DCA3FA16485BA14A0E1E2A8EF8538FE93B60D77BE480B4481E1EE0F5E0066DEC2FCF79F1581A2F88EB05CD8923C243023A4CB89ECA8C740DFF0125D8734E5B9680FD7433A7F18BC4D83A4635ECFBC0E967EC61E2AD34BE30E738CBBEDE60BF79C6FCB5E010000";
                }
                else if (TruckName.Equals("Mercedes Actros"))
                {
                    decode = "1F8B0800E2CC995E00FF5590CB6EC3201045F791F20F288BAA95A2000E609CAFE8A27B3430E386DAAE2D4C5BA95F5FE3F4E1EEAE46E78E8EEE537A0BDD23C4D7BCDFB101E6CE2517C67E4C17767F776EEBCA565E1DD912056A015589D8D42D2AFBF0D378DE34505BAB0C1C99282409AD2A2B7E49BF21BD32E48DFC2651D6B231BA906D0F1D6D40B246A946AFFFB435929AB500B5D2E7E65688D33F695FAB80ABB426D2D4DEFC013550E13DCC7FFFC52AB02A7E102C17B124840C6E827CBDB003476AF93B5D63E889E732171F2805429A4F10721A673E95FDDCCBE8F9671C40BAC185D31CE361BFFB0279D86AF95F010000";
                }
                else if (TruckName.Equals("Mercedes New Actros"))
                {
                    decode = "1F8B0800FECC995E00FF5590CB6EC3201045F791F20F288BAA95A200CE8071BEA28BEED10043436DD796EDB652BFBE86F4E1EEAE46E78E8EEED3F4E6DB474CAFCB7EC77A9C5B3B593F74C37461F777E75857A67270646B144109AC720C4D1D0398879FC6F3A6119431A0F1C844264928A88CF825DD8674A0C969F94D0659CB46AB4CC60E5BDA80643440A3CA3F65B4A4A614B006756E6E8534FE937635F850A41591A278F3C7A09032EF70FEFB2F8A4051FC205C2F624D0117B4232ED70B3BF04091BFD335F98EF892E7E23D4D9E02CD27F4CB34CC9590C0C7BCA17D191CFF4C3D4ADB83F5A739A5C37EF70521AA0E6664010000";
                }
                else if (TruckName.Equals("Renault T"))
                {
                    decode = "1F8B080009CD995E00FF4D90C16EC3201044EF91F20F2887AA91A28031189CAFE8A177B4C0D2503BB185492BF5EB6BDC24F56DB4FB66349AF77473DD1BC46BDE6EC805A6CE24E3867E4827F2FA5207C535B7E24066C9BC64C08BF4AD0A5EE8FDC3F1B17278A9B568E04058219149C1357B9276455AD1A06DAA3BE92B55B58D2C64E8A1C327582D39812B0EBA48E72486A6FE03E3B80A74A295CAF97BA0ABEB16F952D2C2F49FC796FF52E91B61BEB05979C86046C8E713D9518F817EE139BA1E692EF3D08457B8F5F998E95896329F83A53FF102266503C729C6DD76F30B14EF9B5A49010000";
                }
                else if (TruckName.Equals("Renault Premium"))
                {
                    decode = "1F8B080015CD995E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B3498A1A1B6630BE356EAD7D7903EDCDDD5E8DCD1D17D894BDB3D43B8A5FD8E0C3077269A76ECC778218F0F67DFD4AAB6FC48D6C89C6050E7E874E31D574F3F8DD74DC309A5B884236199442678ADD82F6937A4E512ADACBE49573595962293BE870E37202AC9B916E59F50B2425D0AD07071D6F74298FE49DB86B7AE480B4481FEEE0F4E0066DEC2FCF79F1581A2F881B05ED89A1C243013A4EB851CA8434FDFF11ADA1E69CA73D1883758FA749A220E6119E894F7336FA3A59F6180CAC4C9C0690EE1B0DF7D010A13CD5360010000";
                }
                else if (TruckName.Equals("Renault Magnum"))
                {
                    decode = "1F8B080021CD995E00FF5590CB6EC3201045F791F20F288BAA95A2000E609CAFE8A27B349821A136B685ED56EAD7D7387D38BBABD1B9A3A3FB96E6BA7985D04DFB1D8930362699BA6FFB7421CF4F675F16BAB0E24896C89C6450E4E8AAD23BA15F7E1BD74DC349AD8582236199442645A1D91F6937A4150AADE23FA4E325AF94CCA46FA1C10D885A0951C9F59FD48A63B516A014F25CDD0B617890B6A5A8DD2A2D1125FABB3F380998790BE3FF7FB60AAC8A9F08CB852DC9C1046680E9762107EAD0D30FBC85BA453AE5B968C20EE6763A45B87673A4439ECFBCF7967E8508DCA468E0348670D8EFBE01393111805F010000";
                }
                else if (TruckName.Equals("DAF XF105"))
                {
                    decode = "1F8B080026CD995E00FF5590CB6AC3301045F781FC83C8A2B41022C9D1335FD145F766E41925AA9DDAD8EE837E7D2DA50F777719CE1D0EF7697C6DDA47482FF376C3AE30B5F558377DD78F27767F778CB67255507BB644815A4095237A1B51B9879FC679D540ED9C32B067229324B4AA9CF825C38A0CCA5030F29B4469A5373A93B18396562039A394D7E59F7646922F05B04A1FFDAD90867FD2C1AA068BB426D2146FFE801A28F301A6BFFFA20814C57782E522968430433DC07C39B11D478AFC8D2EA9E988CF792E8E100F1F910F79B6FAB90FFC335D41D6709852DA6D375FBC830ABD54010000";
                }
                else if (TruckName.Equals("DAF XF"))
                {
                    decode = "1F8B080097B6AD5E00FF5590CB4EC3301045F795FA0F561708A4AA76523FFB152CD85BE3CC989AA4244A52407C3DB1CB23ECAE46E78E8EEED3786DDA4748AFF376C32E30B57EF44DDFF5E389DDDF1DA3A96D1DE49E2D51A01250E788CE4494F6E1A7F1BC6AA0B2566AD8339149124AD656FC92614506A929E8EA9BC4CA544EAB4CC60E5A5A8164B5944E957FCAEA8A5C298091EAE86E8534FC930E463658A41591A278F3075440990F30FDFD1745A028BE132C17B1248419FC00F3F9C4761C29F2373AA7A6233EE7B938423C7C444FD7B1D77CC8E3F9973EF0CF7481CAA3F6709852DA6D375FB90F4A5A5D010000";
                }
                else if (TruckName.Equals("Scania S"))
                {
                    decode = "1F8B080045CD995E00FF5590CB6EC3201045F791F20F5616552B45051C0670BEA28BEED1004343EDD896715BA95F5FE3F4E1EEAE66CE1D1DCDF3F4E6DB274CFDBCDF5557CCAD9DAC1FBA613A57F777A7A86B533B79AC96C80370AC4B0C8D8E419A879FC6CBA611C018A9F058F1421207591BFE4BBA0DE9A422A7C4371984168D8242C60E5BDA806494940DACF7C02841CD5A402DE1D4DC0A69FC27EDB4F461950622A078F3C70048857798FFEEF3556055FC205C267C490167B423CE9773756081227BA74BF21DB1B9BC8B658F7DC2C76C6B2E141BCBF7ECEBE0D867BAA2B039F77659A674D8EFBE00091103C15F010000";
                }
                else if (TruckName.Equals("Volvo FH 2012"))
                {
                    decode = "1F8B0800E300D85E00FF5590CB6AC3301045F781FC83C8A2B410A2472459CA5774D1BD1859A358B5531BDB75A15F5F4BE9C3DD5D867387C37D19DFEBF619D2DBBCDF911B4CAD1B5DDD77FD78218F0FE7580923BC3C9235B2A018881C83AD6290E6E9A771DD348232466A38129649644A0AC37E49BF21BDD4E835FF2603AFB8D52A93B1831637201A2DA555E59F329AA32D05A8A43ADB7B210DFFA47D25EB50A415A2C278F787A00033EF61FAFBCF8A4051FC40582F6C4D01667003CCCD851C68C048176C52DD219DF35C74E9BBA53FC5866B27181774C8FBB9D7DED3CF7403EE16E1E034A574D8EFBE003338CCD460010000";
                }
                else if (TruckName.Equals("Scania R 2009"))
                {
                    decode = "1F8B08002502D85E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B34C090503BB1056E2BF5EB6B481FEEEE6A74EEE8E8BEA437D73F43BCCDDB0DB942EE4D326E1CC674228F0FC7D0B5AAB57C4F96C8BC60D096E875173C574F3F8DF3AAE185525CC29EB0422213BC55EC97B42BD272895636DFA46FBA464B51C830408F2B1095E45C8BFA4F28D9A0AE05E8B838EA7B214EFFA46DC79DAFD2025160B8FB83178085B790FFFEB32A50153F10960B5B928719CC04F3E54476D463A0EF78896E403A97B96876708B7048742AC399D7D1D2CF7885C6E464E09063DC6D375F8819B90859010000";
                }
                else if (TruckName.Equals("Volvo FH16 2009"))
                {
                    decode = "1F8B0800FA00D85E00FF5590CB6E83301045F791F20F5616552B45B14D3CC6E42BBAE8DE1ADBE3E0420A024AA57E7D31E983EEAE46E78E8EEECBF0EE9B674C6FD37EC76E383676B0BE6BBBE1C21E1FCEB12C4CE1D4912D51041058E418AA3206659E7E1AD74D2380314AE391894C92005518F14BBA0DE99426A7E5371964292B0D998C2D36B401C968A52A58FF81D192AAB580A58273752FA4FE9FB42B950FAB341001C5BB3F0640CABCC3F1EFBF580556C50FC2E52296147042DBE3545FD881078A7CA63AF996F894E7E273D7CEDD29D652F33E4F675F3BC73FD30DA59DAD3F8D291DF6BB2FC4D8EBF55A010000";
                }
                else if (TruckName.Equals("Scania R"))
                {
                    decode = "1F8B08001701D85E00FF5590CB6AC3301045F781FC83C9A2341022C9D1CBF98A2EBA17236994A8766C23B92DF4EB6B394DEBEE2EC3B9C3E1BEA677D7BE40ECA7EDA6BA416E4D326EE88674AE9E9F4E41D5BAB6FC50CD917A41A12ED1372A78AEF78FC665D5F0426B2EE150D1422215BCD6F497B42BD2728956B21FD233C51A290A193A687105A2969C3762F927B464D82C05505C9C9A7B218EFFA4ADE2CE2FD2025160B8FB83178085B790FFFED3456051FC44982F744E1E2630234CD773B5231E03F9C06B741D92A9CC45B2833EC231999A3249C6B29E791B2CF98A376026A7DEC031C7B8DB6EBE0101D913CF5F010000";
                }
                else if (TruckName.Equals("Scania Streamline"))
                {
                    decode = "1F8B0800F9D09D5E00FF5590CB4EC3301045F795FA0F5117884A1576528F1FFD0A16ECAD713CA6264FC50124BE9E38051A765733E78E8EE6657AAF9B678CFDBCDF151DA6C64EB61EDA61BA148F0FE7A02A5D39712A96C83D70AC72F446052FF4F1B7F1BA6978D05A483C153C93C441549AFF916E433A21C9C9F287F4A52A8D844C86161BDA80A4A51006D67BA06549662DA0127036B7421CFF493B256ABF4A031150B8F9A307A4CC3B4CF7FB7C1558153F0997095F92C719ED88F3F5521C98A7C03EE81AEB96D89CDFC5528D7DC4A7344F845D1B7B6263FEA07D1B1CFB8A1D963625BBEC633CEC77DF544C478062010000";
                }
                else
                {
                    MessageBox.Show("We got no painting for this truck.");
                    return;
                }

                string inputData = Utilities.ZipDataUtilitiescs.unzipText(decode);
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TruckPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

                    MessageBox.Show("Paint data  has been inserted.");
                }
                else
                    MessageBox.Show("Wrong data. Expected Paint data but\r\n" + Lines[0] + "\r\nwas found.");
            }
            catch
            {
                MessageBox.Show("Something gone wrong with decoding.");
            }
        }
        //end Share buttons
        //end User Trucks tab
    }
}