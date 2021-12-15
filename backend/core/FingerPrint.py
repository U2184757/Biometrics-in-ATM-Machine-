from PIL import Image,ImageChops

from atm_system.settings import BASE_DIR
from .FaceAuthentication import get_image_from_data_url



def FingerPrintAuthentication(photo,canvasData):

    img1 = BASE_DIR+photo
    img2 = get_image_from_data_url(canvasData[0])

    diff = ImageChops.difference(img1,img2)

    if diff.getbbox():
        return False
    return True