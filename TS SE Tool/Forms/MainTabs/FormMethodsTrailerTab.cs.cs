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
using System.Windows.Forms;

namespace TS_SE_Tool
{
    public partial class FormMain
    {
        //User Trailer tab
        private void CreateTrailerPanelControls()
        {
            CreateTrailerPanelProgressBars();
            CreateTrailerPanelButtons();
        }

        private void CreateTrailerPanelProgressBars()
        {
            int pHeight = RepairImg.Height, pOffset = 5, lOffset = 60, pSizeW = 300;
            int pSkillsNameHeight = 32, pSkillsNameWidth = 32, pSkillsNameOffset = 5, pSkillsNamelOffset = 12;

            string[] toolskillimgtooltip = new string[] { "Cargo", "Body", "Chassis", "Wheels" };
            Label slabel;
            Panel Ppanel;

            for (int i = 0; i < 4; i++)
            {
                slabel = new Label();
                groupBoxUserTrailerTrailerDetails.Controls.Add(slabel);
                slabel.Name = "labelTrailerPartName" + toolskillimgtooltip[i];
                slabel.Location = new Point(pSkillsNamelOffset, 23 + (pSkillsNameHeight + pSkillsNameOffset * 3) * i);
                slabel.Text = toolskillimgtooltip[i];
                slabel.AutoSize = true;

                Panel imgpanel = new Panel();
                groupBoxUserTrailerTrailerDetails.Controls.Add(imgpanel);

                imgpanel.Parent = groupBoxUserTrailerTrailerDetails;
                imgpanel.Location = new Point(pSkillsNamelOffset, 23 + 14 + (pSkillsNameHeight + pSkillsNameOffset * 3) * i);
                imgpanel.BorderStyle = BorderStyle.None;
                imgpanel.Size = new Size(pSkillsNameWidth, pSkillsNameHeight);
                imgpanel.Name = "TrailerPartImg" + i.ToString();

                Bitmap bgimg = new Bitmap(TrailerPartsImg[i], pSkillsNameHeight, pSkillsNameWidth);
                imgpanel.BackgroundImage = bgimg;

                //Panel 
                Ppanel = new Panel();
                groupBoxUserTrailerTrailerDetails.Controls.Add(Ppanel);

                Ppanel.Parent = groupBoxUserTrailerTrailerDetails;
                Ppanel.Location = new Point(lOffset, imgpanel.Location.Y);
                Ppanel.BorderStyle = BorderStyle.FixedSingle;
                Ppanel.Size = new Size(pSizeW, RepairImg.Height);
                Ppanel.Name = "progressbarTrailerPart" + i.ToString();

                Button button = new Button();
                groupBoxUserTrailerTrailerDetails.Controls.Add(button);

                button.Parent = groupBoxUserTrailerTrailerDetails;
                button.Location = new Point(Ppanel.Location.X + Ppanel.Width + pOffset, Ppanel.Location.Y);
                button.FlatStyle = FlatStyle.Flat;
                button.Size = new Size(RepairImg.Height, RepairImg.Height);
                button.Name = "buttonTrailerElRepair" + i.ToString();
                button.BackgroundImage = RepairImg;
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.Text = "";
                button.FlatAppearance.BorderSize = 0;
                button.Click += new EventHandler(buttonTrailerElRepair_Click);
            }
        }

        private void CreateTrailerPanelButtons()
        {
            int pHeight = RepairImg.Height, pOffset = 5, tOffset = comboBoxUserTrailerCompanyTrailers.Location.Y;
            int topbutoffset = comboBoxUserTrailerCompanyTrailers.Location.X + comboBoxUserTrailerCompanyTrailers.Width + pOffset;

            //tableLayoutPanel13

            Button buttonR = new Button();
            //tabPageTrailer.Controls.Add(buttonR);
            tableLayoutPanelUserTruckTrailer.Controls.Add(buttonR, 3, 1);
            buttonR.Location = new Point(topbutoffset, tOffset);
            buttonR.FlatStyle = FlatStyle.Flat;
            buttonR.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonR.Name = "buttonTrailerRepair";
            buttonR.BackgroundImage = RepairImg;
            buttonR.BackgroundImageLayout = ImageLayout.Zoom;
            buttonR.Text = "";
            buttonR.FlatAppearance.BorderSize = 0;
            buttonR.Click += new EventHandler(buttonTrailerRepair_Click);
            buttonR.Dock = DockStyle.Fill;

            Button buttonInfo = new Button();
            //tabPageTrailer.Controls.Add(buttonInfo);
            tableLayoutPanelUserTruckTrailer.Controls.Add(buttonInfo, 0, 1);
            //buttonInfo.Location = new Point(labelUserTrailerTrailer.Location.X + (comboBoxUserTrailerCompanyTrailers.Location.X - labelUserTrailerTrailer.Location.X - CutomizeImg.Width - pOffset) / 2, buttonUserTruckSelectCurrent.Location.Y + pOffset);
            buttonInfo.FlatStyle = FlatStyle.Flat;
            buttonInfo.Size = new Size(CutomizeImg.Width, CutomizeImg.Height);
            buttonInfo.Name = "buttonTrailerInfo";
            buttonInfo.BackgroundImage = ConvertBitmapToGrayscale(CutomizeImg);
            buttonInfo.BackgroundImageLayout = ImageLayout.Zoom;
            buttonInfo.Text = "";
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Enabled = false;
            buttonInfo.Dock = DockStyle.Fill;
        }

        public void buttonTrailerRepair_Click(object sender, EventArgs e)
        {
            string[] PartList = { "trailerdata", "body", "chassis", "tire" };
            string trailerNameless = "";

            trailerNameless = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();

            StartTrailerParts:

            foreach (string tempPart in PartList)
            {
                foreach (UserCompanyTruckDataPart temp in UserTrailerDictionary[trailerNameless].Parts.FindAll(x => x.PartType == tempPart))
                {
                    string partNameless = temp.PartNameless;

                    int i = 0;

                    foreach (string temp2 in temp.PartData)
                    {
                        if (temp2.StartsWith(" wear:"))
                        {
                            UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                            break;
                        }
                        else
                        if (temp2.StartsWith(" cargo_damage:"))
                        {
                            UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "trailerdata").PartData[i] = " cargo_damage: 0";
                            break;
                        }
                        i++;
                    }
                }
            }

            UserCompanyTruckDataPart slavetrailer = UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "slavetrailer");

            if (slavetrailer != null)
            {
                trailerNameless = slavetrailer.PartNameless;
                goto StartTrailerParts;
            }

            UpdateTrailerPanelProgressBars();
        }

        public void buttonTrailerElRepair_Click(object sender, EventArgs e)
        {
            Button curbtn = sender as Button;
            int bi = Convert.ToByte(curbtn.Name.Substring(21));

            string[] PartList = { "trailerdata", "body", "chassis", "tire" };
            string trailerNameless = "";

            trailerNameless = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();

            StartTrailerParts:

            foreach (UserCompanyTruckDataPart temp in UserTrailerDictionary[trailerNameless].Parts.FindAll(x => x.PartType == PartList[bi]))
            {
                string partNameless = temp.PartNameless;

                int i = 0;

                foreach (string temp2 in temp.PartData)
                {
                    if (temp2.StartsWith(" wear:"))
                    {
                        UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                        break;
                    }
                    else
                    if (temp2.StartsWith(" cargo_damage:"))
                    {
                        UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "trailerdata").PartData[i] = " cargo_damage: 0";
                        break;
                    }
                    i++;
                }
            }

            UserCompanyTruckDataPart slavetrailer = UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "slavetrailer");

            if (slavetrailer != null)
            {
                trailerNameless = slavetrailer.PartNameless;
                goto StartTrailerParts;
            }

            UpdateTrailerPanelProgressBars();
        }

        private void UpdateTrailerPanelProgressBars()
        {
            UserTrailerDictionary.TryGetValue(comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTrailer);

            if (SelectedUserCompanyTrailer == null)
                return;

            for (int i = 0; i < 4; i++)
            {
                Panel pnl = null;
                string pnlname = "progressbarTrailerPart" + i.ToString();
                if (groupBoxUserTrailerTrailerDetails.Controls.ContainsKey(pnlname))
                {
                    pnl = groupBoxUserTrailerTrailerDetails.Controls[pnlname] as Panel;
                }

                if (pnl != null)
                {
                    UserCompanyTruckDataPart tempPart = null;

                    switch (i)
                    {
                        case 0:
                            tempPart = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "trailerdata");
                            break;
                        case 1:
                            tempPart = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "body");
                            break;
                        case 2:
                            tempPart = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "chassis");
                            break;
                        case 3:
                            tempPart = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "tire");
                            break;
                    }

                    string wear = "0";
                    decimal _wear = 0;

                    if (tempPart != null)
                        try
                        {
                            List<string> TruckDataPart = tempPart.PartData;
                            wear = TruckDataPart.Find(xl => xl.StartsWith(" wear:") || xl.StartsWith(" cargo_damage:")).Split(new char[] { ' ' })[2];
                        }
                        catch
                        { }

                    if (wear != "0" && wear != "1")
                        try
                        {
                            _wear = Utilities.NumericUtilities.HexFloatToDecimalFloat(wear);
                        }
                        catch
                        {
                            _wear = 1;
                        }
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

            string lctxt = "";
            labelLicensePlateTr.Text = "";
            int stindex = 0, endindex = 0;

            string LicensePlate = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "trailerdata").PartData.Find(xl => xl.StartsWith(" license_plate:")).Split(new char[] { '"' })[1];

            for (int i = 0; i < LicensePlate.Length; i++)//SelectedUserCompanyTruck.LicensePlate.Length; i++)
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
                labelLicensePlateTr.Text = lctxt.Split(new char[] { '|' })[0] + " Country " + lctxt.Split(new char[] { '|' })[1];
            else
                labelLicensePlateTr.Text = lctxt.Split(new char[] { '|' })[0];
        }

        private void FillUserCompanyTrailerList()
        {

            DataTable combDT = new DataTable();
            DataColumn dc = new DataColumn("UserTrailerkNameless", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("UserTrailerName", typeof(string));
            combDT.Columns.Add(dc);


            combDT.Rows.Add("null", "-- NONE --"); //none

            foreach (KeyValuePair<string, UserCompanyTruckData> UserTrailer in UserTrailerDictionary)
            {
                if (UserTrailer.Value.Main)
                {
                    string trailername = "";

                    if (UserTrailerDictionary[UserTrailer.Key].Users)
                        trailername = "[U] ";
                    else
                        trailername = "[Q] ";

                    trailername += UserTrailer.Key;

                    string trailerdef = UserTrailerDictionary[UserTrailer.Key].Parts.Find(x => x.PartType == "trailerdef").PartNameless;

                    /*
                    try
                    {
                        string source_name = UserTrailerDefDictionary[trailerdef].Find(x => x.StartsWith(" source_name:")).Split(':')[1];

                        if (!source_name.Contains("null"))
                        {
                            trailername += source_name.Split(new char[] { '"' })[1].Trim(new char[] { ' ' }) + " | ";
                        }
                    }
                    catch { }
                    */

                    trailername += " [ ";

                    if (UserTrailerDefDictionary.Count > 0)
                    {
                        if (UserTrailerDefDictionary.ContainsKey(trailerdef))
                        {
                            string[] trailerDefPropertys = { "body_type", "axles", "chain_type" };
                            string[] trailerDefExtra = { "{0}", "{0} axles", "{0}" };

                            int iCounter = 0;
                            List<string> CurTrailerDef = UserTrailerDefDictionary[trailerdef];

                            bool wasfound = false;

                            foreach (string Property in trailerDefPropertys)
                            {
                                try
                                {
                                    string tmp = CurTrailerDef.Find(x => x.StartsWith(" " + Property + ":")).Split(':')[1].Trim(new char[] { ' ' });

                                    if (wasfound)
                                        trailername += " ";
                                    trailername += String.Format(trailerDefExtra[iCounter], tmp);

                                    wasfound = true;
                                }
                                catch { wasfound = false; }
                                iCounter++;
                            }
                        }
                        else
                        {
                            trailername += trailerdef;
                        }
                    }
                    else
                    {
                        trailername += trailerdef;
                    }

                    trailername += " ]";

                    combDT.Rows.Add(UserTrailer.Key, trailername);
                }
            }

            if (combDT.Rows.Count > 0)
            {
                //combDT.DefaultView.Sort = "UserTrailerName ASC";
                comboBoxUserTrailerCompanyTrailers.Enabled = true;
                comboBoxUserTrailerCompanyTrailers.ValueMember = "UserTrailerkNameless";
                comboBoxUserTrailerCompanyTrailers.DisplayMember = "UserTrailerName";
                comboBoxUserTrailerCompanyTrailers.DataSource = combDT;
                comboBoxUserTrailerCompanyTrailers.SelectedValue = PlayerDataV.UserCompanyAssignedTrailer;
            }
            else
            {
                comboBoxUserTrailerCompanyTrailers.Enabled = false;
            }
        }

        private void comboBoxCompanyTrailers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbbx = sender as ComboBox;

            if (cmbbx.SelectedIndex != -1 && cmbbx.SelectedValue.ToString() != "null")
            {
                UpdateTrailerPanelProgressBars();
                tableLayoutPanelUserTruckTrailer.Controls.Find("buttonTrailerRepair", false)[0].Enabled = true;
                groupBoxUserTrailerTrailerDetails.Enabled = true;
                groupBoxUserTrailerShareTrailerSettings.Enabled = true;

            }
            else
            {
                tableLayoutPanelUserTruckTrailer.Controls.Find("buttonTrailerRepair", false)[0].Enabled = false;
                groupBoxUserTrailerTrailerDetails.Enabled = false;
                groupBoxUserTrailerShareTrailerSettings.Enabled = false;
            }
        }

        private void buttonUserTrailerSelectCurrent_Click(object sender, EventArgs e)
        {
            comboBoxUserTrailerCompanyTrailers.SelectedValue = PlayerDataV.UserCompanyAssignedTrailer;
        }

        private void buttonUserTrailerSwitchCurrent_Click(object sender, EventArgs e)
        {
            PlayerDataV.UserCompanyAssignedTrailer = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();
        }

        private void buttonTrailersCapricornsPaint_Click(object sender, EventArgs e)
        {
            try
            {

                string trailerdef = UserTrailerDictionary[comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString()].Parts.Find(x => x.PartType == "trailerdef").PartNameless;

                string trailFull = "";
                try
                {
                    string source_name = UserTrailerDefDictionary[trailerdef].Find(x => x.StartsWith(" source_name:")).Split(':')[1];

                    if (!source_name.Contains("null"))
                    {
                        trailFull += source_name.Split(new char[] { '"' })[1].Trim(new char[] { ' ' });
                    }
                }
                catch { }

                string decode = "1F8B08006792995E00FF9590CD8AC2301485F782EF105C88036262D39FD4A77031FB70937B3366ACA63445C5A79FA432C5590E04F241BE732EB99F03F88E8623F8EBB85CB00BC4B31EB40D5D180E6CB396AE295461CA2D4B28B0125064C4B67158AA8FDFC4D79C105B96CFFC62DEBA505949A5CA054649DADB3A63BA5B059413AE8333FD196E1D004EC39BCA89DA4D5897240DBE02BEFF8F6F20BEF743EB8AE973227B56C8469A2A7B77826488440823E81EC6D381AD3892E3373A79DB111F5F8BD3E17E25E4D1C69D090FDEE73DEAEF60F8D35F60AF93B58BDEAF968B1FEF4F44A869010000";
                if (trailFull.Equals("trailer_def.krone.coolliner.single_3.reefer"))
                {
                    decode = "1F8B080010C0995E00FF6D50418AC3300CBC17FA07D3436921D44E9C364E5FB187BD1BD9965B6FDC3838610BFBFA8D1A36F4B030A041D28CC47C660811F307847EDA6ED803C64E676D534CF9CA0E7BE99B4A55A62ED84C853B0BA888BAB6F1AE56C73FC56D55888211D68979F372CA4AAC15191825B1B417A2736D1520297C840E57415930C23208C37F7D0323BE1D80D657AF6705195B211B69CEB4F7449837C4CC1C4CA00798EE57B6E30E3DFFC67BB011F9B404A1D3B347C7BB9C7A3CD994620C3D663E503EFA2B19FE131E509EC61076DBCD2F03D95BF33D010000";
                }
                else if (trailFull.Equals("trailer_def.krone.profiliner.single_3.curtain"))
                {
                    decode = "1F8B080044C0995E00FF4D90C16AC3300C86EF85BE83E9616C506A274E17A74FB1C3EE46B6E5558B1B1B27ACD0A76FBC765940A01F49FF27A1CF0C14307F000DD376C32E30F63A6B1B43CC27F6FA227D5BABDA347B364BE18E02EA225DD77AD7A8B73FC7D7E2107B5662E99815CB292BB151056094C4CABE1739E74E0116870FD0E31A553D513E505AEAD5FF0A0323AE1640E7EBDF6345015B215B698E65EE8A304F88593998402798CE27B6E30E3DFFC133D9807C7A3C42C7EB808EF7390E7848397A0A3460E6A93C487F47C36F74814AA7C348B4DB6EEE3F65122B40010000";
                }
                else if (trailFull.Equals("trailer_def.krone.dryliner.single_3.dryvan"))
                {
                    decode = "1F8B080071C0995E00FF6D50C18AC23010BD0BFE43F0B028149336759BFA157BD87B982413CD3636252D2BBB5F6F47B178101ECC6366DE9BE17D670811F317847E5AAFD805C64E676D534CF9C8B61FD23795AA4C5DB0990A77105011756DE35DAD764FC56951888211968979F172CA4AAC15191825B1B49F44E7DA2A4052F8081D2E82B26084C7200CEFFA06467C3900ADAFEECF0A32B64236D21C68EF8A306F88993998400F309D8F6CC31D7AFE8BE76023F2E911844ED71E1DEF72EA71EFF25F0C3D663E503CFA2719FE1F2E506AB71F43D8AC5737E481114E3E010000";
                }


                string inputData = Utilities.ZipDataUtilitiescs.unzipText(decode);
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TrailerPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTrailerDictionary[comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

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
        //end User Trailer tab
    }
}