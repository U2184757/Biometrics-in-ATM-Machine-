from PIL import Image

import face_recognition
from django.core.files.base import ContentFile

from atm_system.settings import BASE_DIR
import base64, secrets, io


def FaceRegognation(photo,canvasData):
    
    user_image= BASE_DIR+photo

    unknow_image= base64ToImg(canvasData)
    unknow_image = BASE_DIR+f'/media/{unknow_image}'

    print(unknow_image)

    picture_of_me = face_recognition.load_image_file(user_image)
    my_face_encoding = face_recognition.face_encodings(picture_of_me)[0]


    unknown_picture = face_recognition.load_image_file(unknow_image)
    unknown_face_encoding = face_recognition.face_encodings(unknown_picture)[0]


    results = face_recognition.compare_faces([my_face_encoding], unknown_face_encoding)

    if results[0] == True:
        return True
    else:
        return False



def base64ToImg(img):
    imgdata = base64.b64decode(img)
    filename = 'some_image.jpg'  # I assume you have a way of picking unique filenames
    with open(BASE_DIR+f'/media/{filename}', 'wb') as f:
        f.write(imgdata)
    return filename