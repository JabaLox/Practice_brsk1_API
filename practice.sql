PGDMP                      |            PracticStore    16.0    16.0      �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    33162    PracticStore    DATABASE     �   CREATE DATABASE "PracticStore" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "PracticStore";
                postgres    false            �            1259    33182    Category    TABLE     h   CREATE TABLE public."Category" (
    "IdCategory" integer NOT NULL,
    "NameCategory" text NOT NULL
);
    DROP TABLE public."Category";
       public         heap    postgres    false            �            1259    33189    Manufacture    TABLE     q   CREATE TABLE public."Manufacture" (
    "IdManufacture" integer NOT NULL,
    "NameManufacture" text NOT NULL
);
 !   DROP TABLE public."Manufacture";
       public         heap    postgres    false            �            1259    33196    Order    TABLE     x   CREATE TABLE public."Order" (
    "IdOrder" integer NOT NULL,
    "UserOrder" integer NOT NULL,
    "OrderDate" date
);
    DROP TABLE public."Order";
       public         heap    postgres    false            �            1259    33201    OrderProduct    TABLE     �   CREATE TABLE public."OrderProduct" (
    "IdOrder" integer NOT NULL,
    "CountProduct" integer NOT NULL,
    "ArticleProduct" text NOT NULL
);
 "   DROP TABLE public."OrderProduct";
       public         heap    postgres    false            �            1259    33175    Product    TABLE     I  CREATE TABLE public."Product" (
    "ArticleProduct" text NOT NULL,
    "NameProduct" text NOT NULL,
    "DesriptionProduct" text NOT NULL,
    "CostProduct" money NOT NULL,
    "Category" integer NOT NULL,
    "Manufacture" integer NOT NULL,
    "DiscountProduct" integer NOT NULL,
    "CountInStockProduct" integer NOT NULL
);
    DROP TABLE public."Product";
       public         heap    postgres    false            �            1259    33168    Role    TABLE     \   CREATE TABLE public."Role" (
    "IdRole" integer NOT NULL,
    "NameRole" text NOT NULL
);
    DROP TABLE public."Role";
       public         heap    postgres    false            �            1259    33163    User    TABLE     �   CREATE TABLE public."User" (
    "IdUser" integer NOT NULL,
    "NameUser" text NOT NULL,
    "LoginUser" text NOT NULL,
    "PasswordUser" text NOT NULL,
    "RoleUser" integer NOT NULL
);
    DROP TABLE public."User";
       public         heap    postgres    false            �          0    33182    Category 
   TABLE DATA           B   COPY public."Category" ("IdCategory", "NameCategory") FROM stdin;
    public          postgres    false    218   �$       �          0    33189    Manufacture 
   TABLE DATA           K   COPY public."Manufacture" ("IdManufacture", "NameManufacture") FROM stdin;
    public          postgres    false    219   S%       �          0    33196    Order 
   TABLE DATA           F   COPY public."Order" ("IdOrder", "UserOrder", "OrderDate") FROM stdin;
    public          postgres    false    220   �%       �          0    33201    OrderProduct 
   TABLE DATA           U   COPY public."OrderProduct" ("IdOrder", "CountProduct", "ArticleProduct") FROM stdin;
    public          postgres    false    221   �%       �          0    33175    Product 
   TABLE DATA           �   COPY public."Product" ("ArticleProduct", "NameProduct", "DesriptionProduct", "CostProduct", "Category", "Manufacture", "DiscountProduct", "CountInStockProduct") FROM stdin;
    public          postgres    false    217   H&       �          0    33168    Role 
   TABLE DATA           6   COPY public."Role" ("IdRole", "NameRole") FROM stdin;
    public          postgres    false    216   d'       �          0    33163    User 
   TABLE DATA           _   COPY public."User" ("IdUser", "NameUser", "LoginUser", "PasswordUser", "RoleUser") FROM stdin;
    public          postgres    false    215   �'       :           2606    33188    Category Category_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY ("IdCategory");
 D   ALTER TABLE ONLY public."Category" DROP CONSTRAINT "Category_pkey";
       public            postgres    false    218            <           2606    33195    Manufacture Manufacture_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public."Manufacture"
    ADD CONSTRAINT "Manufacture_pkey" PRIMARY KEY ("IdManufacture");
 J   ALTER TABLE ONLY public."Manufacture" DROP CONSTRAINT "Manufacture_pkey";
       public            postgres    false    219            @           2606    33229    OrderProduct OrderProduct_pkey 
   CONSTRAINT     y   ALTER TABLE ONLY public."OrderProduct"
    ADD CONSTRAINT "OrderProduct_pkey" PRIMARY KEY ("ArticleProduct", "IdOrder");
 L   ALTER TABLE ONLY public."OrderProduct" DROP CONSTRAINT "OrderProduct_pkey";
       public            postgres    false    221    221            >           2606    33200    Order Order_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_pkey" PRIMARY KEY ("IdOrder");
 >   ALTER TABLE ONLY public."Order" DROP CONSTRAINT "Order_pkey";
       public            postgres    false    220            8           2606    33181    Product Product_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_pkey" PRIMARY KEY ("ArticleProduct");
 B   ALTER TABLE ONLY public."Product" DROP CONSTRAINT "Product_pkey";
       public            postgres    false    217            6           2606    33172    Role Role_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "Role_pkey" PRIMARY KEY ("IdRole");
 <   ALTER TABLE ONLY public."Role" DROP CONSTRAINT "Role_pkey";
       public            postgres    false    216            2           2606    33174    User User_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY ("IdUser");
 <   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_pkey";
       public            postgres    false    215            4           2606    33247 
   User login 
   CONSTRAINT     N   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT login UNIQUE ("LoginUser");
 6   ALTER TABLE ONLY public."User" DROP CONSTRAINT login;
       public            postgres    false    215            B           2606    33216    Product category    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT category FOREIGN KEY ("Category") REFERENCES public."Category"("IdCategory") NOT VALID;
 <   ALTER TABLE ONLY public."Product" DROP CONSTRAINT category;
       public          postgres    false    217    4666    218            C           2606    33211    Product manufacture    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT manufacture FOREIGN KEY ("Manufacture") REFERENCES public."Manufacture"("IdManufacture") NOT VALID;
 ?   ALTER TABLE ONLY public."Product" DROP CONSTRAINT manufacture;
       public          postgres    false    219    217    4668            E           2606    33230    OrderProduct order    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrderProduct"
    ADD CONSTRAINT "order" FOREIGN KEY ("IdOrder") REFERENCES public."Order"("IdOrder") NOT VALID;
 @   ALTER TABLE ONLY public."OrderProduct" DROP CONSTRAINT "order";
       public          postgres    false    4670    221    220            F           2606    33235    OrderProduct product    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrderProduct"
    ADD CONSTRAINT product FOREIGN KEY ("ArticleProduct") REFERENCES public."Product"("ArticleProduct") NOT VALID;
 @   ALTER TABLE ONLY public."OrderProduct" DROP CONSTRAINT product;
       public          postgres    false    221    217    4664            A           2606    33206 	   User role    FK CONSTRAINT     ~   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT role FOREIGN KEY ("RoleUser") REFERENCES public."Role"("IdRole") NOT VALID;
 5   ALTER TABLE ONLY public."User" DROP CONSTRAINT role;
       public          postgres    false    4662    215    216            D           2606    33221 
   Order user    FK CONSTRAINT     �   ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "user" FOREIGN KEY ("UserOrder") REFERENCES public."User"("IdUser") NOT VALID;
 8   ALTER TABLE ONLY public."Order" DROP CONSTRAINT "user";
       public          postgres    false    220    215    4658            �   u   x�=�A
�PC�3���^�È��p/-O`�O���ɍ:~��0�d.�	�k�����v���-�Zf����s�������.��GZ1���������ߎ�M�3��Q�LU	W�      �   8   x�3���O�,IM��2��q�2��I��/K�2�N�-.�K�2�t,(�I����� L`�      �   6   x�Uɹ  �:��8|م�砍K���88ۈ�	ZTFYS��[y�W���|8z      �   W   x�3�4�vtq�22JR�K��K2�sR�����.��\&�ƨR��P)3N#���������T.s��`G. I�fM� ��      �     x�]��J�@�ϳO�wEv��$+A�-��"^��&mӦ����̓W�'�B�"��'�}#wiE~f������Tհ��pO/'#EC�w\�g��W\�m��O��v�W����1v�=���g�ѦӼ�#~��o'�M>�����;1�qg��;�V��&i���o��tm`Ddq, I��Z�;�O���8w�Q�~�[�_�;�ٳR6E�P�QT!hS���"z��D|��2�*�+9�%��9��2$G��?Ќ{      �   I   x�ʻ�0�:	X�a�'�FA��H<f�#�~wsa���ҵ�U9�|�6"C�҈���s�,�e0���(�      �   �   x�3估�bÅvr��rssq^�pa�-@��#����N#.c��.l��Բ�͘��<�Ӑ˄�$���/�\!�8��h�e
�ML�����E�8�j�sSA��&@��qqq tn6�     