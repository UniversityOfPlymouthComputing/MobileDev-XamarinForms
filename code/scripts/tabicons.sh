#!/bin/zsh
tempname="${1:t:r}_temp.${1:t:e}"
newname="${1:t:r}_square.${1:t:e}"

newsize=${2:=512}

convert $1 -resize ${newsize}x${newsize} $tempname
convert -background none  -gravity center $tempname -extent ${newsize}x${newsize} $newname
rm $tempname

#ios Tabbar Images
if [ ! -d "iOS_tab" ] 
then
	mkdir "iOS_tab"
fi
tabimg="${1:t:r}_ios_tabimg.${1:t:e}"
ios3x="${tabimg:t:r}@3x.${1:t:e}"
ios2x="${tabimg:t:r}@2x.${1:t:e}"
ios="${tabimg:t}"

convert $newname -resize 30x30 "./iOS_tab/$ios"
convert $newname -resize 60x60 "./iOS_tab/$ios2x"
convert $newname -resize 90x90 "./iOS_tab/$ios3x"


#Android Tab Images
# https://iconhandbook.co.uk/reference/chart/android/

if [ ! -d "droid_tab" ]
then
	mkdir "droid_tab"
	cd "droid_tab"
	mkdir "Drawable"
	cd "Drawable"
	mkdir "drawable-ldpi"
	mkdir "drawable-mdpi"
	mkdir "drawable-hdpi"
	mkdir "drawable-xhdpi"
	mkdir "drawable-xxhdpi"
	mkdir "drawable-xxxhdpi"
	cd ..
	cd ..
fi

convert $newname -resize 24x24 "./droid_tab/Drawable/drawable-ldpi/$newname"
convert $newname -resize 32x32 "./droid_tab/Drawable/drawable-mdpi/$newname"
convert $newname -resize 48x48 "./droid_tab/Drawable/drawable-hdpi/$newname"
convert $newname -resize 64x64 "./droid_tab/Drawable/drawable-xhdpi/$newname"
convert $newname -resize 96x96 "./droid_tab/Drawable/drawable-xxhdpi/$newname"
convert $newname -resize 128x128 "./droid_tab/Drawable/drawable-xxxhdpi/$newname"






