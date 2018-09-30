#
# Variables
#

ifeq ($(CONFIGURATION),)
CONFIGURATION = Release
endif

LIBNAME = AsmichiProcessPipeline
SRCDIR = ./
OBJDIR = ../../obj/ProcessPipeline.Native/$(CONFIGURATION)/
SRCS = $(addprefix $(SRCDIR),lib.c)
OBJS = $(addprefix $(OBJDIR),$(SRCS:.c=.o))
CC = gcc

CFLAGS = -g -MMD -std=c11 -Wextra -Werror -fPIC
LDFLAGS = -g -fPIC -shared -Wl,-soname,lib$(LIBNAME).so -Wl,--version-script=$(SRCDIR)$(LIBNAME).version

ifeq ($(CONFIGURATION),Release)
CFLAGS += -O3
else ifeq ($(CONFIGURATION),Debug)
CFLAGS +=
else
$(error Unknown CONFIGURATION: $(CONFIGURATION))
endif

#
# Targets
#
.PHONY : all
all: $(OBJDIR)lib$(LIBNAME).so

-include $(OBJS:.o=.d)

$(OBJDIR)%.o : $(SRCDIR)%.c
	mkdir -p $(dir $@)
	$(CC) $(CFLAGS) -o $@ -c $<

$(OBJDIR)lib$(LIBNAME).so: $(OBJS) $(SRCDIR)$(LIBNAME).version
	mkdir -p $(dir $@)
	$(CC) $(LDFLAGS) -o $@ $(OBJS)
