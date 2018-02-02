# NProfiler (ArcGIS 10.1 +)

The NProfiler is an ArcGIS Add-In to extract normalized profiles and related metrics. It works for ArcGIS version up to 10.1. 

## Instalation
To install them simply double click the Add-In file **N_Profiles.esriAddIn.esriAddIn**. It is not necessary to keep the Add-In file in the computer, it will be copied to *"<user_directory>\Documents\ArcGIS\AddIns"*.

Once installed they should appear in the Add-In Manager of ArcMap , under *Menu > Customize > Add-In Manager*.

<img src="https://geolovic.github.io/NProfiler_ArcGIS/images/install_NProfiler.jpg" width="600" />

To add tool:
1. Go to *Menu > Customize > Customize Mode*. 
2. Select the *Commands tab* and locate the **"Geomorphic Indexes"** category. 
3. Drag and drop the command buttons to any toolbar of ArcMap

## Usage
To use the Add-In simply click on the added button and the program will show an inputbox to select the rivers (line shapefile) and the Digital Elevation Model for elevations (raster).

<img src="https://geolovic.github.io/NProfiler_ArcGIS/images/NProfiler_inputbox.jpg" />

Once the Normalized profiles have been extracted, the main window will show the results. This main window has the following parts:

<img src="https://geolovic.github.io/NProfiler_ArcGIS/images/NProfiler_main_window.jpg" />

1. Combo-box to select between the different rivers (each line in the river shapefile is considered as a single river)
2. Smooth factor for the normalized profile (number between 1-6)
3. Main window with the normalized profile
4. Main parameters related to the normalized profile (note that the image does not have defined MaxC and dL, because it is a convex profile)
5. Buttons to save / export the image or the data

## References
Pérez-Peña, J. V., Al-Awabdeh, M., Azañón, J. M., Galve, J. P., Booth-Rea, G., & Notti, D. (2017). SwathProfiler and NProfiler: Two new ArcGIS Add-ins for the automatic extraction of swath and normalized river profiles. Computers & Geosciences, 104(135), 150. https://doi.org/10.1016/j.cageo.2016.08.008
